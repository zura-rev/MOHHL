using Tasks.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tasks.Core.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> Filter(int id, string categoryName, int parentId);
        Category CreateCategory(Category category);
    }
}
