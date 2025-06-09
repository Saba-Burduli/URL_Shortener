namespace URL_Shortener.Models;

public class CreateUrlRequest
{
    public string? OriginalUrl { get; set; }
    
    public string? CustomAlias { get; set; }

    public DateTime? ExpirationDate { get; set; } = DateTime.Now;

}