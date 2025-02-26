namespace DeveloperStore.SalesApi.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public ICollection<CartItem> Products { get; set; }
}
