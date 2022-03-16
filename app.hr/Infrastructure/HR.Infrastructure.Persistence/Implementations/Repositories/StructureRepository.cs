using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class StructureRepository : IStructureRepository
    {
        protected readonly DataContext context;
        public StructureRepository(DataContext context)
        {
            this.context = context;
        }

        public int Create(Structure position)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Structure Read(int id)
        {
            throw new NotImplementedException();
        }

        public Structure Update(Structure position)
        {
            throw new NotImplementedException();
        }

        IQueryable<Structure> IStructureRepository.Filter(int id, int sectionId, int positionId, double defaultSalary, int count)
        {
            try
            {
                var structures = context.Structures.Where(x =>
                    (id == 0 || x.Id == id) &&
                    (sectionId == 0 || x.Section.Id == sectionId) &&
                    (positionId == 0 || x.Position.Id == positionId) &&
                    (defaultSalary == 0 || x.DefaultSalary == defaultSalary) &&
                    (count == 0 || x.Count == count)
                ).OrderByDescending(x => x.Id);

                return structures;  

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
