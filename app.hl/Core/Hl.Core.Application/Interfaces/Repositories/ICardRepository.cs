using Hl.Core.Domain.Models;
using System;
using System.Linq;

namespace Hl.Core.Application.Interfaces.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        IQueryable<Card> Filter(int id, int callId, int userId, int userType, int status, string note, DateTime performDate, int? categoryId);
        Card GetByCallId(int callId);
        Card UpdateCard(int id, string note);
    }
}
