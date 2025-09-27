using order_management_system.Models;

namespace order_management_system.Repositories.Interfaces;

public interface IOrderRepository
{
    void AddOrder(Order order);
    bool RemoveOrder(Order order);
    Order GetOrder(int orderId);
    IReadOnlyCollection<Order> GetOrders();
    IEnumerable<Order> GetOrdersByDateRange(DateTime from, DateTime to);
}