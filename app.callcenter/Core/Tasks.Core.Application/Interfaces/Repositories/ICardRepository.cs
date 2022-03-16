using Tasks.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.Core.Application.Interfaces.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        IQueryable<Card> Filter(int id, int callId, int userId, int userType, int status, string note, DateTime performDate, int? categoryId);
        Card UpdateCard(int id, string note);
    }
}
