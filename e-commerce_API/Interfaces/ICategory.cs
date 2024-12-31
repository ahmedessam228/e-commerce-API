using e_commerce_API.DTO;
using e_commerce_API.Models;

namespace e_commerce_API.Interfaces
{
    public interface ICategory
    {
        List<CategoryDTO2> GetAll();
        CategoryDTO GetById(int id);
        void Add(CategoryDTO2 categoryDto);
        void Update(CategoryDTO2 categoryDto, int id);
        void Delete(int id);
    }
}
