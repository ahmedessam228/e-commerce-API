using e_commerce_API.Data;
using e_commerce_API.DTO;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace e_commerce_API.Sevices
{
    public class CategoryService : ICategory
    {
        AppDbcontext _context;
     
        public CategoryService(AppDbcontext context)
        {
            _context = context;
        }

        public List<CategoryDTO2> GetAll()
        {

            return _context.Categories
                .Select(category => new CategoryDTO2
                {
                    Id = category.Id,
                    Name = category.Name
                }).ToList();
        }


        public CategoryDTO GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            List<Product> products = _context.Products.Where(x => x.C_id == id).ToList();
            List<ProductDto> productDto = new List<ProductDto>();


            CategoryDTO categoryWithProductList = new CategoryDTO();

            categoryWithProductList.Id = id;
            categoryWithProductList.Name = category.Name;
            foreach (Product product in products)
            {
                productDto.Add(new ProductDto { Id = product.Id, Name = product.Name , Price = product.Price });

            }
            categoryWithProductList.Products = productDto;

            return categoryWithProductList;

            //return new CategoryDTO
            //{
            //    Id = category.Id,
            //    Name = category.Name
            //};

        }

        public void Update(CategoryDTO2 categoryDto, int id)
        {
            var oldCategory = _context.Categories.FirstOrDefault(x => x.Id == id);

            oldCategory.Name = categoryDto.Name;

            _context.Categories.Update(oldCategory);
            _context.SaveChanges();

        }

        public void Add(CategoryDTO2 categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

    }
}
