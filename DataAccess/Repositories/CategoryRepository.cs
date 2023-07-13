using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task Create(Category model)
        {
            await CategoryDAO.Create(model);
        }

        public async Task Delete(Category model)
        {
            await CategoryDAO.Delete(model);
        }

        public async Task Update(Category model)
        {
            await CategoryDAO.Update(model);
        }

        public async Task<Category> FindCategoryById(int id)
        {
            var model = await CategoryDAO.FindCategoryById(id);
            return model;
        }

        public async Task<IQueryable<Category>> GetAllCategory()
        {
            var model = await CategoryDAO.GetAllCategory();
            return model.AsQueryable();
        }
    }
}
