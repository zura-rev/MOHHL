using Hl.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl.Core.Application.Interfaces.Repositories
{
    public interface ICallRepository : IRepository<Call>
    {
        int CreateCall(Call call);
        Call GetById(int id);
        IEnumerable<Call> GetMatchCalls(string phone, string privateNumber, int topValue);
        IQueryable<Call> Filter(
            int id, 
            string callAuthor, 
            string privateNumber, 
            string phone, 
            int? categoryId,  
            //Category category, 
            string note, 
            DateTime createDate, 
            int callStatus, 
            int? userId, 
            DateTime fromDate, 
            DateTime toDate);
    }
}
