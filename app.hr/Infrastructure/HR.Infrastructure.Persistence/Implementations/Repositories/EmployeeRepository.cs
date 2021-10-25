using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context){}
    }
}
