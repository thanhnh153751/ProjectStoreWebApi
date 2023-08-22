using AutoMapper;
using BusinessObjects;
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CustomerDAO
    {

        //public static async Task<List<Category>> GetAllCategory()
        //{
        //    List<Category> list;
        //    try
        //    {
        //        using (var context = new ApplicationDBContext())
        //        {
        //            list = await context.Category.Where(x => x.status == true).ToListAsync();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return list;
        //}

        public static async Task<Customer> AuthenticationCustomer(string userName,string pass)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var model = await context.Customer.SingleOrDefaultAsync(x => x.username == userName && x.password == pass && x.status==1);
                    return model;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task<Customer> GetCustomerById(int customerId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var model = await context.Customer.SingleOrDefaultAsync(x => x.customerId == customerId && x.status == 1);
                    return model;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task Create(Customer b)
        {

            using (var context = new ApplicationDBContext())
            {
                context.Customer.Add(b);
                await context.SaveChangesAsync();
            }
        }

        public static async Task Update(Customer p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Customer>(p).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


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
