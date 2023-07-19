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
    }
}
