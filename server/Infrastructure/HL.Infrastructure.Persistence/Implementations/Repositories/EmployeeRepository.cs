using HL.Core.Application.Interfaces.Repositories;
using HL.Core.Domain.Models;

namespace HL.Infrastructure.Persistence.Implementations.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context){}
    }
}
