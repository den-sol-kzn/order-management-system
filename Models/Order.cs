namespace order_management_system.Models;

public class Order
{
    #region Properties

    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public List<OrderItem> Items { get; set; }

    #endregion

    #region Constructors

    public Order()
    {
        DateCreated = DateTime.Now;
        Items = new List<OrderItem>();
    }

    public Order(int id)
    {
        Id = id;
        DateCreated = DateTime.Now;
        Items = new List<OrderItem>();
    }

    public Order(int id, DateTime dateCreated)
    {
        Id = id;
        DateCreated = dateCreated;
        Items = new List<OrderItem>();
    }

    #endregion

    #region Public Methods

    public void AddItem(Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть положительным числом", nameof(quantity));

        var existingItem = FindItemByProductId(product.Id);

        if (existingItem is not null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            Items.Add(new OrderItem(product, quantity));
        }
    }

    public bool RemoveItem(int productId)
    {
        if (IsEmpty()) return false;

        var index = Items.FindIndex(item => item.Product.Id == productId);

        if (index == -1) return false;

        Items.RemoveAt(index);
        return true;
    }

    public bool UpdateItemQuantity(int productId, int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentException("Количество не может быть отрицательным", nameof(newQuantity));

        if (IsEmpty()) return false;

        var existingItem = FindItemByProductId(productId);

        if (existingItem is null) return false;

        if (newQuantity == 0)
        {
            return RemoveItem(productId);
        }

        existingItem.Quantity = newQuantity;
        return true;
    }

    public decimal GetTotalAmount()
    {
        return Items.Sum(item => item.GetTotalPrice());
    }

    public bool IsEmpty()
    {
        return Items.Count == 0;
    }

    public void Clear()
    {
        Items.Clear();
    }

    public int GetTotalItemsCount()
    {
        return Items.Sum(item => item.Quantity);
    }

    public int GetUniqueProductsCount()
    {
        return Items.Count;
    }

    public override string ToString() => $"Order #{Id} | Date: {DateCreated:g} | Items: {GetUniqueProductsCount()} | Total: {GetTotalAmount()} ₽";

    #endregion

    #region Private Methods

    private OrderItem? FindItemByProductId(int productId)
    {
        return Items.FirstOrDefault(item => item.Product.Id == productId);
    }

    #endregion
}