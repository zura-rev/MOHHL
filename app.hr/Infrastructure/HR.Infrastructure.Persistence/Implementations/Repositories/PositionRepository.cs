using HR.Core.Application.Interfaces.Repositories;
using HR.Core.Domain.Models;

namespace HR.Infrastructure.Persistence.Implementations.Repositories
{
    public class PositionRepository : Repository<Position> , IPositionRepository
    {
        public PositionRepository(DataContext context) : base(context)
        {

        }
    }
}
