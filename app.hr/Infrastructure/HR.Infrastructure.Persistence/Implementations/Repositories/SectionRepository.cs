using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
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

        public Section Create(Section section)
        {
            context.Sections.Add(section);  
            context.SaveChanges();  
            return section;  
        }

        public IQueryable<Section> Read()
        {
            return context.Sections;
        }

        public Section Read(int id)
        {
            return context.Sections.FirstOrDefault(x=>x.Id==id);
        }

        public Section Update(Section section)
        {
            var result = context.Sections.FirstOrDefault(x => x.Id == section.Id);
            if (result != null) 
            {
                result.Id = section.Id;  
                result.SectionName = section.SectionName;
                result.ParentId = section.ParentId;
                context.Sections.Update(result);    
                context.SaveChanges();  
                return result;
            }
            return null;    
        }

        public int Delete(int id)
        {
            var result = context.Sections.FirstOrDefault(x => x.Id == id);
            if (result != null) 
            {
                context.Sections.Remove(result);
                context.SaveChanges();
                return result.Id;   
            }
            return -1;
        }
        
    }
    
}
