using HR.Core.Application.Interfaces;
using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class OrderTypeRepository : IOrderTypeRepository
    {
        protected readonly DataContext context;
        public OrderTypeRepository(DataContext context)
        {
            this.context = context;
        }

        IQueryable<OrderType> IOrderTypeRepository.Filter(int id, string orderTypeName)
        {
            try
            {
                var orderTypes = context.OrderTypes.Where(x =>
                   (id == 0 || x.Id == id) &&
                   (string.IsNullOrWhiteSpace(orderTypeName) || x.OrderTypeName.Contains(orderTypeName)))
                       .OrderByDescending(x => x.Id);
                return orderTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }

    } 
}
