using HL.Core.Application.Interfaces.Repositories;
using HL.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HL.Infrastructure.Persistence.Implementations.Repositories
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
            Category category,
            string note,
            DateTime createDate,
            int callStatus,
            int userId,
            DateTime fromDate,
            DateTime toDate
            )
        {
            try
            {
                var res = context.Calls
                    .Include(x => x.Category)
                    .Include(x => x.Performers)
                    .Include(x => x.User)
                    .Where(x =>
                         (id == 0 || x.Id == id) &&
                         (x.CreateDate > fromDate && x.CreateDate < toDate.AddDays(1)) &&
                         (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber) &&
                         (string.IsNullOrWhiteSpace(callAuthor) || x.CallAuthor == callAuthor) &&
                         (string.IsNullOrWhiteSpace(phone) || x.Phone == phone) &&
                         (string.IsNullOrWhiteSpace(note) || x.Note.Contains(note)) &&
                         (categoryId == null || x.Category.Id == categoryId) &&
                           (string.IsNullOrWhiteSpace(note) || x.Note.Contains(note)))
                     .OrderByDescending(x => x.Id);

                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        IEnumerable<Call> ICallRepository.GetExecutableCalls(string user)
        {
            try
            {
                var res = context.Calls
               .Include(x => x.Category)
               .Include(x => x.Performers)
               .ThenInclude(x => x.User)
               .Where(x => x.User.UserName == user && x.CallStatus != 1)
                .OrderByDescending(x => x.Id);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        IEnumerable<Call> ICallRepository.GetMatchCalls(string phone, string privateNumber)
        {
            try
            {
                    var res = context.Calls
                   .Include(x => x.Category)
                   .Include(x => x.Performers)
                   .ThenInclude(x=>x.User)
                   .Where(x =>
                        (string.IsNullOrWhiteSpace(phone) || x.Phone == phone) &&
                        (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber))
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
                .Include(x => x.Performers)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        int ICallRepository.CreateCall(Call call)
        {
            context.Calls.Add(call);
            context.SaveChanges();
            return call.Id;
        }

    }
}
