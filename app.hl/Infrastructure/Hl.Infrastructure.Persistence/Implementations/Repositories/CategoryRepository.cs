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

        IEnumerable<Category> ICategoryRepository.Filter(int id, string categoryName, int parentId, int status)
        {
            try
            {
                var res = context.Categories
               .Where(x =>
                    (id == 0 || x.Id == id) &&
                    (string.IsNullOrWhiteSpace(categoryName) || x.CategoryName.Contains(categoryName)) &&
                    (parentId == 0 || x.ParentId == parentId) &&
                    (status == 0 || x.Status == status))
                .OrderByDescending(x => x.ParentId);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        Category ICategoryRepository.CreateCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        Category ICategoryRepository.UpdateCategory(int id, Category category)
        {
            var _category = context.Categories.FirstOrDefault(x => x.Id == id);
            context.Entry(_category).CurrentValues.SetValues(category);
            context.SaveChanges();
            return _category;
        }
       
    }
}
