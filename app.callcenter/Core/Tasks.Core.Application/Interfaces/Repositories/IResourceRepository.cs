using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Core.Domain.Models;

namespace Tasks.Core.Application.Interfaces.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<bool> CheckAllAsync(IEnumerable<int> resourceIds);
    }
}
