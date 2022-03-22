using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core.Application.Interfaces.Repositories
{
    public interface ISectionRepository
    {
        IQueryable<Section> Filter(int id, string sectionName, int parentId);
        IQueryable<Section> Read();
        Section Read(int id);
        Section Create(Section section);
        Section Update(Section section);
        int Delete(int Id);
    }
}
