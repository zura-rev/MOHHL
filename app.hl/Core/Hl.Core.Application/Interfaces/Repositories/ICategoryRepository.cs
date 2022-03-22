using Hl.Core.Domain.Models;
using System.Collections.Generic;

namespace Hl.Core.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> Filter(int id, string categoryName, int parentId);
        Category CreateCategory(Category category);
    }
}
