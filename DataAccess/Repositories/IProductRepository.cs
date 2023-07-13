
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;

namespace DataAccess
{
    public interface IProductRepository
    {
        //void SaveProduct(Product p);
        //Product GetProductById(int id);
        //List<Product> GetProductBySearch(string key);
        //List<Product> GetProductBySearchPrice(string key);
        //void DeleteProduct(Product p);
        //void UpdateProduct(Product p);
        //List<Category> GetCategories();
        Task<IQueryable<Product>> GetAllProducts();
        Task<IQueryable<ProductModel>> GetAllProductest();
        public Task<Product> FindProductById(int id);
        public Task<IQueryable<ProductModel>> FindProductByIdOdata(int id);
        public Task Create(Product model);
        public Task Update(Product model);
        public Task Delete(Product model);
    }
}
