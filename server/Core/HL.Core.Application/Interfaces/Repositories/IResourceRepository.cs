using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HL.Core.Domain.Models;

namespace HL.Core.Application.Interfaces.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<bool> CheckAllAsync(IEnumerable<int> resourceIds);
    }
}
