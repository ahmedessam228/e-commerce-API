using e_commerce_API.DTO;
using e_commerce_API.Models;

namespace e_commerce_API.Interfaces
{
    public interface IOrder
    {
        List<OrderDTO> GetAllOrders();
        OrderDTO GetOrderById(int id);
        void AddOrder(OrderDTO order);
        void UpdateOrder(OrderDTO order,int id);
        void DeleteOrder(int id);
    }
}
