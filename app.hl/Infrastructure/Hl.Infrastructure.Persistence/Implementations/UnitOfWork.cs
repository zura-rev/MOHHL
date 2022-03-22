using Hl.Core.Application.Interfaces;
using Hl.Core.Application.Interfaces.Repositories;
using Hl.Infrastructure.Persistence.Implementations.Repositories;
using System.Threading.Tasks;

namespace Hl.Infrastructure.Persistence.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private ICallRepository callRepository;
        private ICardRepository cardRepository;
        private ICategoryRepository categoryRepository;
        private IResourceRepository resourceRepository;
        private IUserRepository userRepository;

        private readonly DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public ICallRepository CallRepository => callRepository ??= new CallRepository(context);
        public ICardRepository CardRepository => cardRepository ??= new CardRepository(context);
        public ICategoryRepository CategoryRepository => categoryRepository ??= new CategoryRepository(context);
        public IResourceRepository ResourceRepository => resourceRepository ??= new ResourceRepository(context);
        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);

        public int Save() => context.SaveChanges();
        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

    }
}
