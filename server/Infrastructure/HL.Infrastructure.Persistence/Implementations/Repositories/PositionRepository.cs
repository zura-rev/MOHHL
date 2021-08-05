using HL.Core.Application.Interfaces.Repositories;
using HL.Core.Domain.Models;

namespace HL.Infrastructure.Persistence.Implementations.Repositories
{
    public class PositionRepository : Repository<Position> , IPositionRepository
    {
        public PositionRepository(DataContext context) : base(context)
        {

        }
    }
}
