using BusinessObjects.Entities;
using DataAccess;
using DataAccess.ModelViewOdata;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public async Task Create(Product model)
        {
            await ProductDAO.Create(model);
        }

        public async Task Delete(Product model)
        {
            await ProductDAO.Delete(model);
        }

        public async Task Update(Product book)
        {
            await ProductDAO.Update(book);
        }

        public async Task<Product> FindProductById(int id)
        {
            var book = await ProductDAO.FindProductById(id);
            return book;
        }

        public async Task<IQueryable<Product>> GetAllProducts()
        {
            var books = await ProductDAO.GetAllProductsAsync();
            return books.AsQueryable();
        }

        public async Task<IQueryable<ProductModel>> GetAllProductest()
        {
            var books = await ProductDAO.GetAllProducts();
            return books.AsQueryable();
        }

        public async Task<IQueryable<ProductModel>> FindProductByIdOdata(int id)
        {
            var book = await ProductDAO.FindProductByIdTest(id);
            return book.AsQueryable();
        }

        public async Task<List<Product>> FindProductByCategoryId(int id)
        {
            return await ProductDAO.FindProductByCategoryId(id);
        }

        public List<Product> ProductTopByView()
        {
            return ProductDAO.FindProductTopByView();
        }



        //public List<Category> GetCategories()
        //{
        //    return CategoryDAO.GetCategories();
        //}

        //public Product GetProductById(int id)
        //{
        //    var product = BookDAO.FindProductById(id);
        //    return product;
        //}

        //public List<Product> GetProductBySearch(string key)
        //{
        //    var list = BookDAO.FindProductBySearch(key);
        //    return list;
        //}

        //public List<Product> GetProductBySearchPrice(string key)
        //{
        //    var list = BookDAO.FindProductBySearchPrice(key);
        //    return list;
        //}

        //public List<Product> GetProducts()
        //{
        //    var list = BookDAO.GetProducts();
        //    return list;
        //}

        //public void SaveProduct(Product p)
        //{
        //    BookDAO.SaveProduct(p);

        //}

        //public void UpdateProduct(Product p)
        //{
        //    BookDAO.UpdateProduct(p);
        //}
    }
}
