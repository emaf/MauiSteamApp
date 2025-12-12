namespace MauiStream.Models;

public class Game
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
    public int DiscountPercent { get; set; }
    public decimal OriginalPrice { get; set; }
    public int FriendCount { get; set; }
    public bool IsAvailableNow { get; set; } = true;
    public string PriceDisplay => DiscountPercent > 0 ? $"${Price:F2} USD" : $"${Price:F2} USD";
    public bool HasDiscount => DiscountPercent > 0;
}
