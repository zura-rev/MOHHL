using Tasks.Core.Application.Interfaces;
using Tasks.Core.Application.Interfaces.Repositories;
using Tasks.Infrastructure.Persistence.Implementations.Repositories;
using System.Threading.Tasks;

namespace Tasks.Infrastructure.Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private ICategoryRepository categoryRepository;
        private ICallRepository callRepository;
        private ICardRepository performerRepository;
        private IResourceRepository resourceRepository;
        private IUserRepository userRepository;


        private readonly DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public ICallRepository CallRepository
        {
            get
            {
                return callRepository == null ? new CallRepository(context) : callRepository;
            }
        }

        public ICardRepository CardRepository => performerRepository ??= new CardRepository(context);
        public ICategoryRepository CategoryRepository => categoryRepository ??= new CategoryRepository(context);


        public IResourceRepository ResourceRepository => resourceRepository ??= new ResourceRepository(context);
        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);


        public int Save() => context.SaveChanges();
        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

    }
}
