using BusinessObjects;
using BusinessObjects.Entities;
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
                            orderDate = null,
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
                        //Save
                        await context.SaveChangesAsync();
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
                        await context.SaveChangesAsync();
                    }

                    // Calculate the totalmoney for the order based on the quantity and unitPrice of each product
                    var openNewOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");
                    var orderTotal = context.OrdersDetail
                        .Where(od => od.orderId == openNewOrder.orderId)
                        .Sum(od => od.quantity * od.unitPrice);
                    openNewOrder.totalmoney = orderTotal;

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

        public static async Task<int> GetSizeCart(int customerId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var openOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");
                    int size = 0;
                    if (openOrder != null)
                    {
                        size = await context.OrdersDetail.Where(od => od.orderId == openOrder.orderId).CountAsync();
                    }
                    return size;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task<List<int>> GetAllYearInOrderDate()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                 var years = await context.Order
                .Where(o => o.orderDate.HasValue) // Lọc chỉ lấy các bản ghi có orderDate không null
                .Select(o => o.orderDate.Value.Year)
                .Distinct()
                .ToListAsync();

                    return years;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public static async Task<List<int>> GetRevenueForMonth(int month,int year)
        {
            using (var context = new ApplicationDBContext())
            {
                var startDate = new DateTime(year, month, 1);
                var endDate = startDate.AddMonths(1).AddTicks(-1);

                var revenueByDate = await context.Order
                    .Where(o => o.orderDate >= startDate && o.orderDate <= endDate && o.totalmoney.HasValue)
                    .GroupBy(o => o.orderDate.Value.Date)
                    .Select(g => new { Date = g.Key.Date, Revenue = g.Sum(o => o.totalmoney.Value) })
                    .ToDictionaryAsync(item => item.Date, item => item.Revenue);

                // Create a list to store the revenue for each day in the month
                var revenueList = new List<int>();

                // Iterate through all days in the month and check if revenue is available for that day
                for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                {
                    var currentDate = new DateTime(year, month, day);
                    var revenue = revenueByDate.ContainsKey(currentDate.Date) ? revenueByDate[currentDate.Date] : 0;
                    revenueList.Add(revenue);
                }

                return revenueList;
            }

        }

        public static async Task<List<OrdersDetail>> GetCartByCustomerId(int customerId)
        {

            using (var context = new ApplicationDBContext())
            {
                var openOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");
                if (openOrder != null)
                {
                    context.Product.ToList();
                    context.Order.ToList();
                    var list = await context.OrdersDetail.Where(x => x.orderId == openOrder.orderId).ToListAsync();
                    return list;
                }
                return new List<OrdersDetail>();
            }
        }

        public static async Task<bool> ChangeQuantityInCart(int customerId, int productId, bool sign)
        {

            using (var context = new ApplicationDBContext())
            {
                var openOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");
                if (openOrder != null)
                {
                    // sing +
                    if (sign)
                    {
                        var unitInStock = context.Product.Find(productId).unitsInStock;
                        var model = await context.OrdersDetail.SingleOrDefaultAsync(x => x.orderId == openOrder.orderId && x.productId == productId);
                        if (model.quantity < unitInStock)
                        {
                            model.quantity += 1;
                            await context.SaveChangesAsync();
                        }
                    }
                    // sing -
                    else
                    {
                        var model = await context.OrdersDetail.SingleOrDefaultAsync(x => x.orderId == openOrder.orderId && x.productId == productId);
                        if (model.quantity == 1)
                        {
                            context.OrdersDetail.Remove(model);

                        }
                        else
                        {
                            model.quantity -= 1;
                        }
                        await context.SaveChangesAsync();
                    }
                    // caculate totak
                    var orderTotal = context.OrdersDetail
                        .Where(od => od.orderId == openOrder.orderId)
                        .Sum(od => od.quantity * od.unitPrice);
                    openOrder.totalmoney = orderTotal;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public static async Task<bool> RemoveFromCart(int customerId, int productId)
        {

            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var openOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");
                    if (openOrder != null)
                    {
                        var model = await context.OrdersDetail.SingleOrDefaultAsync(x => x.orderId == openOrder.orderId && x.productId == productId);

                        context.OrdersDetail.Remove(model);
                        await context.SaveChangesAsync();

                        // caculate totak
                        var orderTotal = context.OrdersDetail
                            .Where(od => od.orderId == openOrder.orderId)
                            .Sum(od => od.quantity * od.unitPrice);
                        openOrder.totalmoney = orderTotal;
                        await context.SaveChangesAsync();
                        return true;
                    }                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public static async Task SentRequestOrder(int customerId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var openOrder = context.Order.FirstOrDefault(o => o.customerId == customerId && o.status == "processing");
                    if (openOrder != null)
                    {
                        openOrder.status = "pending";
                        await context.SaveChangesAsync();
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<bool> ApprovalRequestOrderByOrderId(int orderId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var openOrder = context.Order.FirstOrDefault(o => o.orderId == orderId && o.status == "pending");
                    if (openOrder != null)
                    {
                        openOrder.status = "approval";
                        openOrder.orderDate = DateTime.Now;
                        await context.SaveChangesAsync();
                        return true;
                    }

                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static async Task<List<Order>> GetAllHistoryOrderByCumtomerId(int customerId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Customer.ToList();
                    var openOrder = await context.Order.Where(o => o.customerId == customerId && (o.status == "pending" || o.status == "approval") ).ToListAsync();
                    return openOrder;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<List<Order>> GetAllHistoryOrderApprpved()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Customer.ToList();
                    var openOrder = await context.Order.Where(o => o.status == "approval").ToListAsync();
                    return openOrder;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<List<Order>> GetAllHistoryOrderPending()
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Customer.ToList();
                    var openOrder = await context.Order.Where(o => o.status == "pending").ToListAsync();
                    return openOrder;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<List<OrdersDetail>> GetOrderDetailByOrderId(int orderId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Product.ToList();
                    var openOrder = await context.OrdersDetail.Where(o => o.orderId == orderId).ToListAsync();
                    return openOrder;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<bool> DeleteOrderByOrderId(int orderId)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var openOrder = await context.Order.SingleOrDefaultAsync(o => o.orderId == orderId && o.status == "pending");
                    if (openOrder != null)
                    {
                        var openOrderDetail = await context.OrdersDetail.Where(o => o.orderId == orderId).ToListAsync();
                        context.OrdersDetail.RemoveRange(openOrderDetail);
                        context.SaveChanges();

                        context.Order.Remove(openOrder);
                        context.SaveChanges();
                        return true;
                    }
                    else return false;                   
                                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
