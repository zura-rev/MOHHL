using HL.Core.Application.Interfaces.Repositories;
using HL.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HL.Infrastructure.Persistence.Implementations.Repositories
{
    public class PerformerRepository : Repository<Performer>, IPerformerRepository
    {
        public PerformerRepository(DataContext context) : base(context) { }

        IQueryable<Performer> IPerformerRepository.Filter(int id, int callId, int userId, int userType, 
            int status, string note, DateTime performDate)
        {
            try
            {
                var res = context.Performers
                    .Include(x => x.Call).ThenInclude(x=>x.User)
                    //.Include(x => x.User)
                    .Where(x => x.UserType == 2 &&
                         (id == 0 || x.Id == id) &&
                         //(x.CreateDate > fromDate && x.CreateDate < toDate.AddDays(1)) &&
                         //(userType==0 || x.UserType == userType) &&
                         (userId == 0 || x.User.Id == userId) &&
                         (callId == 0 || x.Call.Id == callId) &&
                         (status == 0 || x.Status == status) &&
                         (string.IsNullOrWhiteSpace(note) || x.Note.Contains(note)))
                     .OrderByDescending(x => x.Id);
                return res;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //IEnumerable<Call> ICallRepository.GetExecutableCalls(string user)
        //{
        //    try
        //    {
        //        var res = context.Calls
        //       .Include(x => x.Category)
        //       .Include(x => x.Performers)
        //       .ThenInclude(x => x.User)
        //       .Where(x => x.User.UserName == user && x.CallStatus != 1)
        //        .OrderByDescending(x => x.Id);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //IEnumerable<Call> ICallRepository.GetMatchCalls(string phone, string privateNumber)
        //{
        //    try
        //    {
        //            var res = context.Calls
        //           .Include(x => x.Category)
        //           .Include(x => x.Performers)
        //           .ThenInclude(x=>x.User)
        //           .Where(x =>
        //                (string.IsNullOrWhiteSpace(phone) || x.Phone == phone) &&
        //                (string.IsNullOrWhiteSpace(privateNumber) || x.PrivateNumber == privateNumber))
        //            .OrderByDescending(x => x.Id);
        //            return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //Call ICallRepository.GetById(int id)
        //{
        //    return context.Calls
        //        .Include(x => x.Category)
        //        .Include(x => x.Performers)
        //        .ThenInclude(x => x.User)
        //        .FirstOrDefault(x => x.Id == id);
        //}

        //int ICallRepository.CreateCall(Call call)
        //{
        //    context.Calls.Add(call);
        //    context.SaveChanges();
        //    return call.Id;
        //}

    }
}
