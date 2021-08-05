using HL.Core.Application.Interfaces.Repositories;
using System.Threading.Tasks;

namespace HL.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IPositionRepository PositionRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public ICallRepository CallRepository { get; }
        public IPerformerRepository PerformerRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IResourceRepository ResourceRepository { get; }
        public IUserRepository UserRepository { get; }

        public int Save();
        public Task<int> SaveAsync();
    }
}
