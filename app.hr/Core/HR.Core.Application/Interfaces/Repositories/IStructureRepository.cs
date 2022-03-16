using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core.Application.Interfaces.Repositories
{
    public interface IStructureRepository
    {
        IQueryable<Structure> Filter(int id, int sectionId, int positionId, double defaultSalary, int count);
        Structure Read(int id);
        int Create(Structure position);
        Structure Update(Structure position);
        int Delete(int Id);
    }
}
