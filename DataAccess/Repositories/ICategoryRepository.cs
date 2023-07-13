
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;

namespace DataAccess
{
    public interface ICategoryRepository
    {
        Task<IQueryable<Category>> GetAllCategory();
        public Task<Category> FindCategoryById(int id);
        public Task Create(Category model);
        public Task Update(Category model);
        public Task Delete(Category model);
    }
}
