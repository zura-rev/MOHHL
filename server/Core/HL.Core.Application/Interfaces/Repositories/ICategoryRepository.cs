using HL.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL.Core.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category CreateCategory(Category category);
        IEnumerable<Category> Filter(int id, string categoryName, int parentId);
    }
}
