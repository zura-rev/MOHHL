using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;
using System;
using System.Linq;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        protected readonly DataContext context;
        public SectionRepository(DataContext context)
        {
            this.context = context; 
        }

        public int Create(Section position)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Section> Filter(int id, string sectionName, int parentId)
        {
            try
            {
                var sections = context.Sections.Where(x =>
                   (id == 0 || x.Id == id) &&
                   (string.IsNullOrWhiteSpace(sectionName) || x.SectionName.Contains(sectionName)))
                       .OrderByDescending(x => x.Id);
                return sections;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Section Read(int id)
        {
            throw new NotImplementedException();
        }

        public Section Update(Section position)
        {
            throw new NotImplementedException();
        }
    }
    
}
