using e_commerce_API.Models;

namespace e_commerce_API.Interfaces
{
    public interface IProduct
    {
        List<Product> GetAll();
        List<Product> GetAllById(int id);
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product,int id);
        void Delete(int id);
    }
}
