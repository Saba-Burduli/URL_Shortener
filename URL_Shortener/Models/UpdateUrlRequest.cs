namespace URL_Shortener.Models;

public class UpdateUrlRequest
{
    public string OriginalUrl { get; set; }
    
    public DateTime ExpirationDate { get; set; } = DateTime.Now;
}