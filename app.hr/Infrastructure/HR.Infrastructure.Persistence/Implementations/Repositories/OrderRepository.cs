using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        protected readonly DataContext context;
        public OrderRepository(DataContext context) 
        {
            this.context = context;
        }

        IQueryable<Order> IOrderRepository.Filter(int id, int orderTypeId, int employeeId, int structureId, double realSalary, DateTime startDate, DateTime endDate)
        {
            try
            {
                var res = context.Orders
                   .Where(x =>
                         (id == 0 || x.Id == id) &&
                         (orderTypeId == 0 || x.OrderType.Id == orderTypeId) &&
                         (employeeId == 0 || x.Employee.Id == employeeId) &&
                         (structureId == 0 || x.Structure.Id == structureId) &&
                         (realSalary == 0 || x.RealSalary == realSalary))
                     .OrderByDescending(x => x.Id);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Order Read(int id)
        {
            return context.Orders.FirstOrDefault(x => x.Id == id);
        }

        public int Create(Order order)
        {
            context.Orders.Add(order);  
            context.SaveChanges();  
            return order.Id;
        }

        public Order Update(Order order)
        {
            var result = context.Orders.FirstOrDefault(x=>x.Id==order.Id);
            if (result != null)
            {
                result.Id = order.Id;
                result.OrderType = order.OrderType;
                result.StartDate = order.StartDate;
                result.EndDate = order.EndDate;
                result.RealSalary = order.RealSalary;
                result.Employee = order.Employee;
                result.Structure = order.Structure;
                context.Orders.UpdateRange(result);
                context.SaveChanges();
                return result;
            } 
            return null;
        }

        public int Delete(int id)
        {
            var order = context.Orders.FirstOrDefault(x=>x.Id==id);
            context.Orders.Remove(order);
            context.SaveChanges();
            return order.Id;
        }
    }
}
