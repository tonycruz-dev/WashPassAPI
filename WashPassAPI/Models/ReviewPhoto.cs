namespace WashPassAPI.Models;

public class ReviewPhoto
{
    public int Id { get; set; }

    public int ReviewId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Review Review { get; set; } = null!;
}