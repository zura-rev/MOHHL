using Hl.Core.Application.Interfaces.Repositories;
using Hl.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hl.Infrastructure.Persistence.Implementations.Repositories
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(DataContext context) : base(context) { }
        IQueryable<Card> ICardRepository.Filter(int id, int callId, int userId, int userType, 
            int status, string note, DateTime performDate, int? categoryId)
        {
            try
            {
                var res = context.Cards
                    .Include(x=>x.User)
                    .Include(x => x.Call)
                    .ThenInclude(x=>x.Category)
                    .Include(x => x.Call)
                    .ThenInclude(x=>x.User)
                    .Where(x => x.UserType == 2 &&
                         (id == 0 || x.Id == id) &&
                         //(x.CreateDate > fromDate && x.CreateDate < toDate.AddDays(1)) &&
                         //(userType==0 || x.UserType == userType) &&
                         (userId == 0 || x.User.Id == userId) &&
                         (callId == 0 || x.Call.Id == callId) &&
                         (status == 0 || x.Status == status) &&
                         (categoryId == null || x.Call.Category.Id == categoryId) &&
                         (string.IsNullOrWhiteSpace(note) || x.Note.Contains(note)))
                     .OrderByDescending(x => x.Id);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        Card ICardRepository.UpdateCard(int id, string note) 
        {
            try
            {
                var card = context.Cards.FirstOrDefault(x => x.CallId==id);
                card.Status = 1;
                card.PerformDate = DateTime.Now;
                card.Note = note;
                context.SaveChanges();
                return card;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
