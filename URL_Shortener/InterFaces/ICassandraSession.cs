using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Models;

namespace URL_Shortener.InterFaces;

public interface ICassandraSession
{
    Task<UrlEntity> InsertUrlAsync(UrlEntity url);
    Task<UrlEntity> GetUrlAsync(string code);
    Task<UrlEntity> UpdateUrlAsync(string code, UpdateUrlRequest request);
    Task<bool> DelateUrlAsync(string code);
    Task IncrementClickAsync(string code);
    Task SaveAnalyticsAsync(string code, string userAgent, string ip);
    Task DeactivateExpiredUrlsAsync();
    
}