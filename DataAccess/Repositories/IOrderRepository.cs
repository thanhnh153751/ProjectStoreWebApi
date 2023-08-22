
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;

namespace DataAccess
{
    public interface IOrderRepository
    {
        Task<List<OrdersDetail>> GetCartByCustomerId(int customerId);
        Task<List<OrdersDetail>> GetOrderDetailByOrderId(int orderId);
        Task<List<Order>> GetAllHistoryOrderByCumtomerId(int customerId);
        Task<List<Order>> GetAllHistoryOrderApprpved();
        Task<List<Order>> GetAllHistoryOrderPending();
        public Task<bool> DeleteOrderByOrderId(int orderId);
        public Task<bool> ApprovalRequestOrderByOrderId(int orderId);
        public Task<bool> AddToCart(int productId, int customerId);
        public Task<bool> ChangeQuantityInCart(int customerId, int productId, bool sign);
        public Task<bool> RemoveFromCart(int customerId, int productId);
        public Task<int> GetSizeCart(int customerId);
        public Task<List<int>> GetAllYearInOrderDate();
        public Task<List<int>> GetRevenueForMonth(int month, int year);
        public Task SentRequestOrder(int customerId);



    }
}
