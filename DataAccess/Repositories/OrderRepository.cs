using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task<bool> AddToCart(int productId, int customerId)
        {
            return await OrderDAO.AddToCart(productId, customerId);
        }

        public async Task<bool> ApprovalRequestOrderByOrderId(int orderId)
        {
            return await OrderDAO.ApprovalRequestOrderByOrderId(orderId);
        }

        public async Task<bool> ChangeQuantityInCart(int customerId, int productId, bool sign)
        {
            return await OrderDAO.ChangeQuantityInCart(customerId, productId,sign);
        }

        public async Task<bool> DeleteOrderByOrderId(int orderId)
        {
            return await OrderDAO.DeleteOrderByOrderId(orderId);
        }

        public async Task<List<Order>> GetAllHistoryOrderApprpved()
        {
            return await OrderDAO.GetAllHistoryOrderApprpved();
        }

        public async Task<List<Order>> GetAllHistoryOrderByCumtomerId(int customerId)
        {
            return await OrderDAO.GetAllHistoryOrderByCumtomerId(customerId);
        }

        public async Task<List<Order>> GetAllHistoryOrderPending()
        {
            return await OrderDAO.GetAllHistoryOrderPending();
        }

        public async Task<List<int>> GetAllYearInOrderDate()
        {
            return await OrderDAO.GetAllYearInOrderDate();
        }

        public async Task<List<OrdersDetail>> GetCartByCustomerId(int customerId)
        {
            return await OrderDAO.GetCartByCustomerId(customerId);
        }

        public async Task<List<OrdersDetail>> GetOrderDetailByOrderId(int orderId)
        {
            return await OrderDAO.GetOrderDetailByOrderId(orderId);
        }

        public async Task<List<int>> GetRevenueForMonth(int month, int year)
        {
            return await OrderDAO.GetRevenueForMonth(month,year);
        }

        public async Task<int> GetSizeCart(int customerId)
        {
            return await OrderDAO.GetSizeCart(customerId);
        }

        public async Task<bool> RemoveFromCart(int customerId, int productId)
        {
            return await OrderDAO.RemoveFromCart(customerId,productId);
        }

        public async Task SentRequestOrder(int customerId)
        {
            await OrderDAO.SentRequestOrder(customerId);
        }
    }
}
