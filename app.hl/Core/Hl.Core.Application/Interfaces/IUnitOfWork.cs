using Hl.Core.Application.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Hl.Core.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ICallRepository CallRepository { get; }
        public ICardRepository CardRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IResourceRepository ResourceRepository { get; }
        public IUserRepository UserRepository { get; }

        public int Save();
        public Task<int> SaveAsync();
    }
}
