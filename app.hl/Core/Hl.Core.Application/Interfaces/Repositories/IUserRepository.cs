using System.Linq;
using System.Threading.Tasks;
using Hl.Core.Domain.Models;
namespace Hl.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IQueryable<User>> Filter(string userName, string privateNumber, string firstName, string lastName);
        Task<IQueryable<User>> GetOperators();
        User GetUserByUserName(string userName);
        User GetUserById(int userId);
        User CreateUser(User user);
        User UpdateUser(int id, User user);
    }
}
