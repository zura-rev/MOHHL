using HL.Core.Application.Interfaces;
using HL.Core.Application.Interfaces.Repositories;
using HL.Infrastructure.Persistence.Implementations.Repositories;
using System.Threading.Tasks;

namespace HL.Infrastructure.Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IPositionRepository positionRepository;
        private IEmployeeRepository employeeRepository;

        private ICategoryRepository categoryRepository;
        private ICallRepository callRepository;
        private IPerformerRepository performerRepository;
        private IResourceRepository resourceRepository;
        private IUserRepository userRepository;


        private readonly DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public IPositionRepository PositionRepository => positionRepository ??= new PositionRepository(context);
        public IEmployeeRepository EmployeeRepository => employeeRepository ??= new EmployeeRepository(context);

        public ICallRepository CallRepository
        {
            get
            {
                return callRepository == null ? new CallRepository(context) : callRepository;
            }
        }

        public IPerformerRepository PerformerRepository => performerRepository ??= new PerformerRepository(context);
        public ICategoryRepository CategoryRepository => categoryRepository ??= new CategoryRepository(context);


        public IResourceRepository ResourceRepository => resourceRepository ??= new ResourceRepository(context);
        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);


        public int Save() => context.SaveChanges();
        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

    }
}
