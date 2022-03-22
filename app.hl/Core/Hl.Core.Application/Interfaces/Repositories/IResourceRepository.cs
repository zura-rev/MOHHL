using System.Collections.Generic;
using System.Threading.Tasks;
using Hl.Core.Domain.Models;

namespace Hl.Core.Application.Interfaces.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<bool> CheckAllAsync(IEnumerable<int> resourceIds);
    }
}
