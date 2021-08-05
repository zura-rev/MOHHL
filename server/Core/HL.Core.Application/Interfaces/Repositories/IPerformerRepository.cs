using HL.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL.Core.Application.Interfaces.Repositories
{
    public interface IPerformerRepository : IRepository<Performer>
    {
        IQueryable<Performer> Filter(int id, int callId, int userId, int userType, int status, string note, DateTime performDate);
    }
}
