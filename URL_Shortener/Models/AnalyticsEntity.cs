namespace URL_Shortener.Models;

public class AnalyticsEntity
{
    public string ShortCode { get; set; }
    
    public string ClickDate { get; set; }

    public string UserAgent { get; set; }

    public string IpAddress { get; set; }

    //string ShortCode
    // 
    // DateTime ClickDate
    // 
    // string UserAgent
    // 
    // string IpAddress
}