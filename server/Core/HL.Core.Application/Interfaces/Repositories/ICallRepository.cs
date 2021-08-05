using HL.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.Core.Application.Interfaces.Repositories
{
    public interface ICallRepository : IRepository<Call>
    {
        int CreateCall(Call call);
        Call GetById(int id);
        //IEnumerable<Call> GetExecutableCalls(string user);
        IEnumerable<Call> GetMatchCalls(string phone, string privateNumber);
        IQueryable<Call> Filter(int id, string callAuthor, string privateNumber, string phone, int? categoryId,  Category category, string note, DateTime createDate, int callStatus, int userId, DateTime fromDate, DateTime toDate);
    }
}
