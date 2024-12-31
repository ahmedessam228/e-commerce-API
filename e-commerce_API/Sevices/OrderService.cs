using e_commerce_API.Data;
using e_commerce_API.DTO;
using e_commerce_API.Interfaces;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_API.Sevices
{

    public class OrderService : IOrder
    {
        private readonly AppDbcontext _context;

        public OrderService(AppDbcontext context)
        {
            _context = context;
        }

        public List<OrderDTO> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.ApplicationUser)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Select(o => new OrderDTO
                {
                    OrderID = o.Id,
                    UserID = o.UserId,
                    UserName = o.ApplicationUser.UserName,
                    OrderDate = o.CreatedDate,
                    TotalAmount = o.OrderDetails.Sum(od => od.Quantity * od.Price),
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                    {
                        OrderDetailID = od.Id,
                        ProductID=od.ProductId,
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        UnitPrice = od.Price
                    }).ToList()
                }).ToList();
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _context.Orders
                .Include(o => o.ApplicationUser)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return null;

            return new OrderDTO
            {
                OrderID = order.Id,
                UserID = order.UserId,
                UserName = order.ApplicationUser.UserName,
                OrderDate = order.CreatedDate,
                TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.Price),
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderDetailID = od.Id,
                    ProductID = od.ProductId,
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    UnitPrice = od.Price
                }).ToList()
            };
        }

        public void AddOrder(OrderDTO orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserID,
                CreatedDate = orderDto.OrderDate,
                OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetails
                {
                    ProductId = od.ProductID,
                    Quantity = od.Quantity,
                    Price = od.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder( OrderDTO orderDto, int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return;

            order.UserId = orderDto.UserID;
            order.CreatedDate = orderDto.OrderDate;

           
            _context.OrderDetails.RemoveRange(order.OrderDetails); 
            order.OrderDetails = orderDto.OrderDetails.Select(od => new OrderDetails
            {
                ProductId = od.ProductID,
                Quantity = od.Quantity,
                Price = od.UnitPrice
            }).ToList();

            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(x=>x.Id==id);
            if (order != null)
            { 
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }

}
