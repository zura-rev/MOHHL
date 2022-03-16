using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core.Application.Interfaces.Repositories
{
    public interface IOrderRepository 
    {
        IQueryable<Order> Filter(int id,int orderTypeId,int employeeId,int structureId,double realSalary,DateTime startDate,DateTime endDate);
        int Create(Order employee);
        Order Read(int id);
        Order Update(Order employee);
        int Delete(int id);
    }
}
