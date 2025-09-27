namespace order_management_system.Models;

public class Order
{
    public int Id { get; private set; }
    public DateTime DateCreated { get; private set; }
    public List<string> Items { get; private set; }

    public Order(int id)
    {
        Id = id;
        DateCreated = DateTime.Now;
        Items = new List<string>();
    }

    public Order(int id, DateTime dateCreated)
    {
        Id = id;
        DateCreated = dateCreated;
        Items = new List<string>();
    }
}