using e_commerce_API.DTO;
using e_commerce_API.Models;

namespace e_commerce_API.Interfaces
{
    public interface IOrderDetails
    {
        List<OrderDetailDTO> GetOrderDetailsByOrderId(int orderId);
        void AddOrderDetail(OrderDetailDTO orderDetail);
        void UpdateOrderDetail(OrderDetailDTO orderDetail,int id);
        void DeleteOrderDetail(int id);
    }
}
