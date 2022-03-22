using Hl.Core.Application.Interfaces.Repositories;
using Hl.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl.Infrastructure.Persistence.Implementations.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        Category ICategoryRepository.CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        IEnumerable<Category> ICategoryRepository.Filter(int id, string categoryName, int parentId)
        {
            try
            {
                var res = context.Categories
               .Where(x =>
                    (id == 0 || x.Id == id) &&
                    (string.IsNullOrWhiteSpace(categoryName) || x.CategoryName.Contains(categoryName)) &&
                    (parentId == 0 || x.ParentId == parentId))
                .OrderByDescending(x => x.Id);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
