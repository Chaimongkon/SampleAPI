using SampleAPI.Data;
using SampleAPI.Models;

namespace SampleAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly SampleDBContext _dbContext;

        public ProductService(SampleDBContext context)
        {
            _dbContext = context;
        }

        public void CreateProduct(Product prodoct)
        {
            Product pro = new Product();
            pro.Name = prodoct.Name;
            pro.Description = prodoct.Description;
            pro.Quantity = prodoct.Quantity;
            pro.Price = prodoct.Price;

            _dbContext.Products.Add(pro);
            _dbContext.SaveChanges();
            }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product is null)
                return;
            
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            var products = _dbContext.Products.ToList();

            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _dbContext.Products.Find(id);

            if(product is null)
                product = new Product();
            
                return product;
            
        }

        public void UpdateProduct(int id, Product product)
        {
            var pro = _dbContext.Products.Find(id);

            if (pro is null)
                return;
            
            pro.Name = product.Name;
            pro.Description = product.Description;
            pro.Quantity = product.Quantity;
            pro.Price = product.Price;

            _dbContext.SaveChanges();
        }

    }
}
