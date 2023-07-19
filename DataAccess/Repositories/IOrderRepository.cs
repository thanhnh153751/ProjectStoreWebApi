
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;

namespace DataAccess
{
    public interface IOrderRepository
    {
        //Task<IQueryable<Category>> GetAllCategory();
        public Task<bool> AddToCart(int productId, int customerId);
        
    }
}
