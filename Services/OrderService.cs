using order_management_system.Models;

namespace order_management_system.Services;

public class OrderService
{
    private readonly List<Order> _orders = new();
    private int _nextOrderId = 1;
}