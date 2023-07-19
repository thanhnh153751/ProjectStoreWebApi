using AutoMapper;
using BusinessObjects;
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CategoryDAO
    {
        private readonly IMapper mapper;

        public CategoryDAO(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public static async Task<List<Category>> GetAllCategory()
        {
            List<Category> list;
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    list = await context.Category.Where(x => x.status == true).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static async Task<Category> FindCategoryById(int id)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var model = await context.Category.SingleOrDefaultAsync(x => x.categoryId == id && x.status==true);
                    return model;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task Create(Category b)
        {

            using (var context = new ApplicationDBContext())
            {
                context.Category.Add(b);
                await context.SaveChangesAsync();
            }
        }

        public static async Task Update(Category p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Category>(p).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static async Task Delete(Category p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var p1 = context.Category.SingleOrDefault(
                    c => c.categoryId == p.categoryId);
                    p1.status = false;
                    //context.Category.Remove(p1);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }      
    }

}
