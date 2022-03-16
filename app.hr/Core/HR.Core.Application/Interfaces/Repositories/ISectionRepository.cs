using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR.Core.Application.Interfaces.Repositories
{
    public  interface ISectionRepository
    {
        IQueryable<Section> Filter(int id, string sectionName, int parentId);
        Section Read(int id);
        int Create(Section position);
        Section Update(Section position);
        int Delete(int Id);
    }
}
