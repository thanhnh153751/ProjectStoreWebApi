using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;

namespace Repositories
{
    public class BlogRepository : IBlogRepository
    {
        public async Task Create(Blog model)
        {
            await BlogDAO.Create(model);
        }

        public async Task<Blog> FindBlogById(int id)
        {
            var model = await BlogDAO.FindBlogById(id);
            return model;
        }

        public async Task<IQueryable<Blog>> GetAllBlogs()
        {
            var model = await BlogDAO.GetAllBlogs();
            return model.AsQueryable();
        }

        public Task Update(Blog model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Blog model)
        {
            throw new NotImplementedException();
        }

    }
}
