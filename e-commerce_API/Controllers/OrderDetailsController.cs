using e_commerce_API.DTO;
using e_commerce_API.Interfaces;
using e_commerce_API.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetails _orderDetailsService;

        public OrderDetailsController(IOrderDetails orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = _orderDetailsService.GetOrderDetailsByOrderId(orderId);
            return Ok(new 
            {
                Message = "Order is Exist",
                orderDetails

            });
        }
        [HttpPost]
        public IActionResult AddOrderDetail([FromBody] OrderDetailDTO orderDetailDto)
        {
            _orderDetailsService.AddOrderDetail(orderDetailDto);
            return Ok(new
            {
                Message = "Add Successfully",
                orderDetailDto
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id, [FromBody] OrderDetailDTO orderDetailDto)
        {
            _orderDetailsService.UpdateOrderDetail(orderDetailDto,id);
            return Ok(new
            {
                Message ="Update Successfully"
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            _orderDetailsService.DeleteOrderDetail(id);
            return NoContent();
        }
    }

}
