using System.Linq;
using System.Threading.Tasks;
using Tasks.Core.Domain.Models;
namespace Tasks.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IQueryable<User>> Filter(string privateNumber, string firstName, string lastName);
        User GetUserByUserName(string userName);
        User GetUserById(int userId);
    }
}
