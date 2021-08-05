﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HL.Core.Application.Interfaces.Repositories;
using HL.Core.Domain.Models;

namespace HL.Infrastructure.Persistence.Implementations.Repositories
{
    internal class ResourceRepository : Repository<Resource>, IResourceRepository
    {
        public ResourceRepository(DataContext context) : base(context) { }


        public async Task<bool> CheckAllAsync(IEnumerable<int> resourceIds)
        {
            var existsIds = await context.Resources.Select(x => x.Id).ToListAsync();
            return await Task.Run(() => resourceIds.All(x => existsIds.Any(y => y == x)));
        }
    }
}
