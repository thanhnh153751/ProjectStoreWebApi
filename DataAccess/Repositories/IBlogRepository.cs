
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;

namespace DataAccess
{
    public interface IBlogRepository
    {
        Task<IQueryable<Blog>> GetAllBlogs();
        public Task<Blog> FindBlogById(int id);
        public Task Create(Blog model);
        public Task Update(Blog model);
        public Task Delete(Blog model);
    }
}
