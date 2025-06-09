namespace URL_Shortener.Models;

public class UrlEntity
{
    public string? ShortCode { get; set; }

    public string? OriginalUrl { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? ExpirationDate { get; set; } = DateTime.Now;

    public int? ClickCount { get; set; }

    public bool? IsActive { get; set; }
    
}