using System.Linq;
using System.Threading.Tasks;
using HL.Core.Domain.Models;
namespace HL.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IQueryable<User>> Filter(string privateNumber, string firstName, string lastName);
        User GetUserByUserName(string userName);
        User GetUserById(int userId);
    }
}
