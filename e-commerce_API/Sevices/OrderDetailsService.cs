using e_commerce_API.Data;
using e_commerce_API.DTO;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_API.Sevices
{
    public class OrderDetailsService : IOrderDetails
    {
        private readonly AppDbcontext _context;

        public OrderDetailsService(AppDbcontext context)
        {
            _context = context;
        }

        public List<OrderDetailDTO> GetOrderDetailsByOrderId(int orderId)
        {
            return _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.O_id == orderId)
                .Select(od => new OrderDetailDTO
                {
                    OrderDetailID = od.Id,
                    ProductID = od.ProductId,
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    UnitPrice = od.Price
                }).ToList();
        }

        public void AddOrderDetail(OrderDetailDTO orderDetailDto)
        {
            var orderDetail = new OrderDetails
            {
                O_id = orderDetailDto.OrderDetailID,
                ProductId = orderDetailDto.ProductID,
                Quantity = orderDetailDto.Quantity,
                Price = orderDetailDto.UnitPrice
            };

            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void UpdateOrderDetail( OrderDetailDTO orderDetailDto, int id)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(x=>x.Id==id);
            if (orderDetail == null) return;

            orderDetail.ProductId = orderDetailDto.ProductID;
            orderDetail.Quantity = orderDetailDto.Quantity;
            orderDetail.Price = orderDetailDto.UnitPrice;

            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(int id)
        {
            var orderDetail = _context.OrderDetails.Find(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
            }
        }
    }
}
