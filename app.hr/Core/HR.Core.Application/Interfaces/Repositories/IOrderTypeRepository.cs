using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core.Application.Interfaces.Repositories
{
    public interface IOrderTypeRepository
    {
        IQueryable<OrderType> Filter(int id, string orderTypeName);
    }
}
