using HR.Core.Domain.Models;
using System.Linq;

namespace HR.Core.Application.Interfaces.Repositories
{
    public interface IPositionRepository
    {
        IQueryable<Position> Filter(int id, string positionName, int sortId);
        Position Read(int id);
        int Create(Position position);
        Position Update(Position position);
        int Delete(int Id);
    }
}
