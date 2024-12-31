using e_commerce_API.Data;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce_API.Sevices
{
    public class ProductService : IProduct
    {
        AppDbcontext _context;
        public ProductService(AppDbcontext context)
        {
            _context = context;
        }


        public  List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetAllById(int id)
        {
            return _context.Products.Where(x => x.C_id == id).ToList();
        }

        public  Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Product product, int id)
        {
            Product oldproduct = GetById(id);

            oldproduct.Name = product.Name; 
            oldproduct.Price = product.Price;
            oldproduct.Description = product.Description;
            oldproduct.C_id = product.C_id;

            _context.SaveChanges(); 

        }
       
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = GetById(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

    }
}
