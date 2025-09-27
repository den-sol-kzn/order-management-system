using order_management_system.Models;
using order_management_system.Repositories.Interfaces;

namespace order_management_system.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();
    private int _nextOrderId = 1;

    public void AddOrder(Order order)
    {
        if (order.Id is 0)
        {
            order.Id = _nextOrderId++;
        }

        if (order.DateCreated == default)
        {
            order.DateCreated = DateTime.Now;
        }

        _orders.Add(order);
    }

    public bool RemoveOrder(Order order)
    {
        return _orders.Remove(order);
    }

    public Order GetOrder(int orderId)
    {
        ValidateOrdersCollection();

        return _orders.FirstOrDefault(o => o.Id == orderId) ??
               throw new KeyNotFoundException($"Заказ с ID {orderId} не найден");
    }

    public IReadOnlyCollection<Order> GetOrders() => _orders;

    public IEnumerable<Order> GetOrdersByDateRange(DateTime from, DateTime to)
    {
        ValidateOrdersCollection();

        return _orders.Where(order => order.DateCreated >= from && order.DateCreated <= to);
    }

    private void ValidateOrdersCollection()
    {
        switch (_orders)
        {
            case null:
                throw new InvalidOperationException("Коллекция заказов не инициализирована.");
            case { Count: 0 }:
                throw new InvalidOperationException("Коллекция заказов пуста.");
        }
    }
}