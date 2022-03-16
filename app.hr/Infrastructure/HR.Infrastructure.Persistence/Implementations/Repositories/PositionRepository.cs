using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;
using System;
using System.Linq;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        protected readonly DataContext context;
        public PositionRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Position> Filter(int id, string positionName, int sortId)
        {
            return context.Positions.Where(x => (id == 0 || x.Id == id) && (string.IsNullOrWhiteSpace(positionName) || x.PositionName == positionName));
        }

        public Position Read(int id)
        {
            return context.Positions.FirstOrDefault(x => x.Id == id);
        }

        public int Create(Position position)
        {
            context.Positions.Add(position);
            context.SaveChanges();
            return position.Id;
        }

        public int Delete(int id)
        {
            var position = context.Positions.FirstOrDefault(x => x.Id == id);
            position.DateDeleted = DateTime.Now;
            context.Positions.Update(position);
            context.SaveChanges();
            return position.Id;
        }

        public Position Update(Position position)
        {
            var result = context.Positions.FirstOrDefault(x => x.Id == position.Id);
            if (result != null) 
            { 
                result.Id = position.Id;    
                result.PositionName = position.PositionName;    
                result.SortId = position.SortId;
                context.Positions.Update(result);
                context.SaveChanges();  
                return result;
            } 
            return null;
        }
    }
}
