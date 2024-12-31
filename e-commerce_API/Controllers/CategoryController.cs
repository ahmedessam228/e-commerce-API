using e_commerce_API.Data;
using e_commerce_API.DTO;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategory _category;
        AppDbcontext context = new AppDbcontext();
        public CategoryController(ICategory category)
        {
            _category = category;
        }

        [Authorize]
        [HttpGet("getAll")]
        public IActionResult GetAllProduct()
        {
            List<CategoryDTO2> categories = _category.GetAll();
            return Ok(new
            {
                Message = "All Product Avillable",
                Products = categories
            });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var categoryItem = _category.GetById(id);

            if (categoryItem != null)
            {
                return Ok(new
                {
                    message = "Category is Exist",
                    categoryItem
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Category  is Not Exist",
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public IActionResult PutCategory(CategoryDTO2 category, int id)
        {
            if (ModelState.IsValid)
            {
                _category.Update(category, id);
                return Ok(new
                {
                    Message = " Update Successfully"
                });

            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult AddCategory(CategoryDTO2 category)
        {
            if (ModelState.IsValid)
            {
                _category.Add(category);
                return Ok(new { Message = " Add Successfully" });
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _category.Delete(id);
            return Ok(new { Message = " Delete Successfully" });
        }

    }
}
