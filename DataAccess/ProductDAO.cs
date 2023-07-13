using AutoMapper;
using AutoMapper.AspNet.OData;
using BusinessObjects;
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess
{
    public class ProductDAO
    {
        private readonly IMapper mapper;

        public ProductDAO(IMapper mapper) 
        {
            this.mapper = mapper;
        }
        public static async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> list;
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Category.ToList();
                    list = await context.Product.ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static async Task<List<ProductModel>> GetAllProducts()
        {
            List<ProductModel> list;
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Category.ToList();
                    var products = await context.Product.Where(x => x.status == true).ToListAsync();
                    list = Mapping.Mapper.Map<List<ProductModel>>(products);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }


        public static async Task<List<ProductModel>> FindProductByIdTest(int id)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Category.ToList();
                    var products = await context.Product.Where(x => x.productId == id && x.status == true).ToListAsync();
                    var list = Mapping.Mapper.Map<List<ProductModel>>(products);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task<Product> FindProductById(int id)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var list = await context.Product.SingleOrDefaultAsync(x => x.productId == id);
                    return list;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task Create(Product b)
        {

            using (var context = new ApplicationDBContext())
            {
                context.Product.Add(b);
                await context.SaveChangesAsync();
            }
        }

        public static async Task Update(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Product>(p).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static async Task Delete(Product p)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var p1 = context.Product.SingleOrDefault(
                    c => c.productId == p.productId);
                    //context.Product.Remove(p1);
                    p1.status = false;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public static List<Product> FindProductBySearch(string key)
        //{
        //    var listProducts = new List<Product>();
        //    decimal price;
        //    try
        //    {
        //        //if (decimal.TryParse(key,out price))
        //        //{
        //        //    using (var context = new Prn231As1Context())
        //        //    {
        //        //        listProducts = context.Products.Where(p => p.UnitPrice == price).ToList();
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    using (var context = new Prn231As1Context())
        //        //    {
        //        //        listProducts = context.Products.Where(p => p.ProductName.Contains(key)).ToList();
        //        //    }
        //        //}
        //        using (var context = new Prn231As1Context())
        //        {
        //            listProducts = context.Products.Where(p => p.ProductName.Contains(key)).ToList();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return listProducts;
        //}

        //public static List<Product> FindProductBySearchPrice(string key)
        //{
        //    var listProducts = new List<Product>();
        //    decimal price;
        //    try
        //    {
        //        if (decimal.TryParse(key, out price))
        //        {
        //            using (var context = new Prn231As1Context())
        //            {
        //                listProducts = context.Products.Where(p => p.UnitPrice == price).ToList();
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    return listProducts;
        //}





        //public void InsertOrUpdate(Product p)
        //{
        //    using (var context = new ApplicationDBContext())
        //    {
        //        context.Entry(p).State = p.ProductId == 0 ?
        //                                   EntityState.Added :
        //                                   EntityState.Modified;

        //        context.SaveChanges();
        //    }
        //}
    }

}
