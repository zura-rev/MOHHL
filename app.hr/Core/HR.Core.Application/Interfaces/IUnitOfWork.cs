using HR.Core.Application.Interfaces.Repositories;
using System.Threading.Tasks;

namespace HR.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IPositionRepository PositionRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderTypeRepository OrderTypeRepository { get; }
        public ISectionRepository SectionRepository { get; }
        public IStructureRepository StructureRepository { get; }

        public int Save();
        public Task<int> SaveAsync();
    }
}
