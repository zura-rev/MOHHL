using Hl.Core.Application.Interfaces.Repositories;
using Hl.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl.Infrastructure.Persistence.Implementations.Repositories
{
    public class CallRepository : Repository<Call>, ICallRepository
    {
        public CallRepository(DataContext context) : base(context) { }

        IQueryable<Call> ICallRepository.Filter(
            int id,
            string callAuthor,
            string privateNumber,
            string phone,
            int? categoryId,
            string note,
            DateTime createDate,
            int callType,
            int? userId,
            DateTime fromDate,
            DateTime toDate
            )
        {
            try
            {
                var res = context.Calls
                    .Include(x => x.Category)
                    .Include(x => x.User)
                    .Include(x => x.Card)
                    .ThenInclude(x => x.User)
                    .Where(x =>
                         (id == 0 || x.Id == id) &&
                         (x.CreateDate > fromDate && x.CreateDate < toDate.AddDays(1)) &&
                         (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber) &&
                         (string.IsNullOrWhiteSpace(callAuthor) || x.CallAuthor == callAuthor) &&
                         (string.IsNullOrWhiteSpace(phone) || x.Phone == phone) &&
                         (categoryId == null || x.Category.Id == categoryId) &&
                         (userId == null || x.User.Id == userId) &&
                         (string.IsNullOrWhiteSpace(note) || x.Note.Contains(note)))
                     .OrderByDescending(x => x.Id);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        Call ICallRepository.GetById(int id)
        {
            return context.Calls
                .Include(x => x.Category)
                .Include(x => x.User)
                .Include(x => x.Card)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        int ICallRepository.CreateCall(Call call)
        {
            context.Calls.Add(call);
            context.SaveChanges();
            return call.Id;
        }

        int ICallRepository.UpdateCall(int id, Call call)
        {
            var _call = context.Calls.FirstOrDefault(x => x.Id == id);
            _call.Id = call.Id;
            _call.Phone = call.Phone;
            _call.PrivateNumber = call.PrivateNumber;
            _call.Note = call.Note;
            _call.Category = call.Category;
            _call.CreateDate = call.CreateDate;
            _call.CallAuthor = call.CallAuthor;
            _call.CallType = call.CallType;
            _call.Card = call.Card;
            _call.User = call.User;
            //context.Entry(_call).CurrentValues.SetValues(call);
            context.Update(_call);
            context.SaveChanges();
            return call.Id;
        }

        IEnumerable<Call> ICallRepository.GetMatchCalls(string phone, string privateNumber, int topValue )
        {
            try
            {
                    var res = context.Calls
                   .Include(x => x.Category)
                   .Include(x => x.Card).ThenInclude(x=>x.User)
                   .Include(x=>x.User)
                   .Where(x =>
                        (string.IsNullOrWhiteSpace(phone) || x.Phone == phone) &&
                        (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber))
                    .OrderByDescending(x => x.Id).Take(topValue);
                    return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public override void Create (Call call)
        //{
        //    context.Calls.Add(call);
        //    context.SaveChanges();
        //}

    }
}
