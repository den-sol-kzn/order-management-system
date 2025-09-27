using order_management_system.Models;
using order_management_system.Repositories.Interfaces;

namespace order_management_system.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public Order GetOrderById(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentOutOfRangeException(nameof(orderId), "ID заказа должен быть положительным числом");

        return _orderRepository.GetOrder(orderId);
    }

    public List<Order> GetOrdersInRange(DateTime from, DateTime to)
    {
        if (from > to)
            throw new ArgumentException($"Начальная дата ({from:g}) не может быть больше конечной ({to:g}).");

        return _orderRepository
            .GetOrdersByDateRange(from, to)
            .OrderByDescending(order => order.DateCreated)
            .ThenBy(order => order.Id)
            .ToList();
    }

    public void PlaceOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (order.IsEmpty())
            throw new InvalidOperationException("Невозможно разместить пустой заказ");
        
        _orderRepository.AddOrder(order);
    }
    
    public IReadOnlyCollection<(string NameProduct, int TotalQuantity)> GetTopProducts(int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество должно быть положительным числом");

        var allOrders = _orderRepository.GetOrders();

        if (allOrders.Count is 0)
            return new List<(string NameProduct, int TotalQuantity)>();

        return allOrders
            .SelectMany(order => order.Items)
            .GroupBy(item => item.Product)
            .Select(group => (Product: group.Key, TotalQuantity: group.Sum(item => item.Quantity)))
            .OrderByDescending(x => x.TotalQuantity)
            .Take(count)
            .Select(x => (NameProduct: x.Product.Name, x.TotalQuantity))
            .ToList();
    }

    public bool CancelOrder(int orderId)
    {
        var order = GetOrderById(orderId);

        ArgumentNullException.ThrowIfNull(order);

        return _orderRepository.RemoveOrder(order);
    }
}