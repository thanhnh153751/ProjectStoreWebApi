using AutoMapper;
using BusinessObjects;
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BlogDAO
    {
        public static async Task<List<Blog>> GetAllBlogs()
        {
            List<Blog> list;
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    list = await context.Blog.Where(x => x.status == true).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static async Task<Blog> FindBlogById(int id)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var model = await context.Blog.SingleOrDefaultAsync(x => x.blogId == id && x.status==true);
                    return model;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task Create(Blog b)
        {

            using (var context = new ApplicationDBContext())
            {
                context.Blog.Add(b);
                await context.SaveChangesAsync();
            }
        }

        //public static async Task Update(Category p)
        //{
        //    try
        //    {
        //        using (var context = new ApplicationDBContext())
        //        {
        //            context.Entry<Category>(p).State =
        //                Microsoft.EntityFrameworkCore.EntityState.Modified;
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}


        //public static async Task Delete(Category p)
        //{
        //    try
        //    {
        //        using (var context = new ApplicationDBContext())
        //        {
        //            var p1 = context.Category.SingleOrDefault(
        //            c => c.categoryId == p.categoryId);
        //            p1.status = false;
        //            //context.Category.Remove(p1);
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}      
    }

}
