using SampleAPI.Models;

namespace SampleAPI.Service
{
    public interface IProductService
    {
        void CreateProduct(Product prodoct);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(int id);
        Product GetProduct(int id);
       List<Product> GetAllProducts();
    }
}
