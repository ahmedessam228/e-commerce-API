using e_commerce_API.DTO;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using e_commerce_API.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderService;

        public OrdersController(IOrder orderService)
        {
            _orderService = orderService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order != null)
            {
                return Ok(new
                {
                    message = "Order is Exist",
                    order
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Order not is Exist",
                    
                });
            }

           
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddOrder([FromBody] OrderDTO orderDto)
        {
            _orderService.AddOrder(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.OrderID }, orderDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDTO orderDto)
        {
            _orderService.UpdateOrder(orderDto,id );
            return Ok(new
            {
                Message ="Update Succesfully"
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.DeleteOrder(id);
            return Ok(new
            {
                Message= "Delete Successfuly"
            });
        }
    }
}
