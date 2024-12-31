using e_commerce_API.Data;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace e_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProduct _product;
        AppDbcontext context = new AppDbcontext();
        public ProductController(IProduct product)
        {
            _product = product;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            List<Product> products = _product.GetAll();
            return Ok(new
            {
                Message = "All Product Avillable",
                Products = products
            });
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Product productItm = _product.GetById(id);

            if (productItm != null)
            {
                return Ok(new
                {
                    message = "Product is Exist",
                    productItm
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Product  is Not Exist",
                });
            }

        }

        [Authorize]
        [HttpGet("getProductsWithinCategory/{id:int}")]
        public IActionResult Getall(int id)
        {
            List<Product> products = _product.GetAllById(id);
            return Ok(new
            {
                Message = "All Product Avillable in this Category",
                Products = products
            });

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public IActionResult PutProduct(Product product, int id)
        {
            if (ModelState.IsValid)
            {
                _product.Update(product, id);
                return Ok(new
                {
                    Message = " Update Successfully"
                });

            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _product.Add(product);
                return Ok(new { Message = " Add Successfully" });
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _product.Delete(id);
            return Ok(new { Message = " Delete Successfully" });
        }
    }
}
