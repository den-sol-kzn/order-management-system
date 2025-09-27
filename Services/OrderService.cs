using order_management_system.Models;

namespace order_management_system.Services;

public class OrderService
{
    private readonly List<Order> _orders = new();
    private int _nextOrderId = 1;

    public IReadOnlyCollection<Order> GetAllOrders() => _orders;

    public Order GetOrderById(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentOutOfRangeException(nameof(orderId), "ID заказа должен быть положительным числом");

        ValidateOrdersCollection();

        return _orders.FirstOrDefault(o => o.Id == orderId)
               ?? throw new KeyNotFoundException($"Заказ с ID {orderId} не найден");
    }

    public List<Order> GetOrdersInRange(DateTime from, DateTime to)
    {
        if (from > to)
            throw new ArgumentException($"Начальная дата ({from:g}) не может быть больше конечной ({to:g}).");

        ValidateOrdersCollection();

        return _orders
            .Where(order => order.DateCreated >= from && order.DateCreated <= to)
            .OrderByDescending(order => order.DateCreated)
            .ThenBy(order => order.Id)
            .ToList();
    }

    public void PlaceOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (order.IsEmpty())
            throw new InvalidOperationException("Невозможно разместить пустой заказ");

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
    
    public IReadOnlyCollection<Product> GetTopProducts(int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество должно быть положительным числом");

        if (_orders.Count is 0)
            return new List<Product>();

        return _orders
            .SelectMany(order => order.Items)
            .GroupBy(item => item.Product)
            .Select(group => (Product: group.Key, TotalQuantity: group.Sum(item => item.Quantity)))
            .OrderByDescending(x => x.TotalQuantity)
            .Take(count)
            .Select(x => x.Product)
            .ToList();
    }
    
    public bool CancelOrder(int orderId)
    {
        var order = GetOrderById(orderId);
        return _orders.Remove(order);
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