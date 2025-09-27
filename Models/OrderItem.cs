namespace order_management_system.Models;

public class OrderItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public OrderItem(Product product, int quantity)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Quantity = quantity;
    }
}