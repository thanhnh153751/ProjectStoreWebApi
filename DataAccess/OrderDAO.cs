using AutoMapper;
using BusinessObjects;
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrderDAO
    {
        public static async Task<bool> AddToCart(int productId, int customerId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    // Check if the customer already has an open order (status: "processing")
                    var openOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");

                    if (openOrder == null)
                    {
                        // Create a new order if the customer doesn't have an open order
                        var newOrder = new Order
                        {
                            customerId = customerId,
                            orderDate = DateTime.Now,
                            requiredDate = DateTime.Now,
                            status = "processing",
                            totalmoney = 0 // Initialize totalmoney to 0 for now
                        };
                        context.Order.Add(newOrder);

                        // Save changes to generate the orderId
                        await context.SaveChangesAsync();

                        // Use the generated orderId to create the OrdersDetail entry
                        var newOrderDetail = new OrdersDetail
                        {
                            orderId = newOrder.orderId,
                            productId = productId,
                            quantity = 1, // Set initial quantity to 1 for new product
                            unitPrice = context.Product.Find(productId).unitPrice
                        };
                        context.OrdersDetail.Add(newOrderDetail);
                    }
                    else
                    {
                        // If the customer has an open order, check if the product already exists in the order
                        var existingOrderDetail = context.OrdersDetail.FirstOrDefault(od => od.orderId == openOrder.orderId && od.productId == productId);

                        if (existingOrderDetail == null)
                        {
                            // If the product does not exist, add it as a new line item with quantity = 1
                            var newOrderDetail = new OrdersDetail
                            {
                                orderId = openOrder.orderId,
                                productId = productId,
                                quantity = 1, // Set initial quantity to 1 for new product
                                unitPrice = context.Product.Find(productId).unitPrice
                            };
                            context.OrdersDetail.Add(newOrderDetail);
                        }
                        else
                        {
                            // If the product already exists, increase the quantity by 1
                            existingOrderDetail.quantity += 1;
                        }
                    }

                    // Calculate the totalmoney for the order based on the quantity and unitPrice of each product
                    var orderTotal = context.OrdersDetail
                        .Where(od => od.orderId == openOrder.orderId)
                        .Sum(od => od.quantity * od.unitPrice);
                    openOrder.totalmoney = orderTotal;

                    // Save changes to the database
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true; // Return true to indicate successful processing
        }

        //public static async Task<Category> FindCategoryById(int id)
        //{
        //    try
        //    {
        //        using (var context = new ApplicationDBContext())
        //        {
        //            var model = await context.Category.SingleOrDefaultAsync(x => x.categoryId == id && x.status==true);
        //            return model;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //}

        //public static async Task Create(Category b)
        //{

        //    using (var context = new ApplicationDBContext())
        //    {
        //        context.Category.Add(b);
        //        await context.SaveChangesAsync();
        //    }
        //}

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
