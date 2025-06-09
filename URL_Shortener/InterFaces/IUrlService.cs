using URL_Shortener.Models;

namespace URL_Shortener.InterFaces;

public interface IUrlService
{
    Task<string> RedirectAsync(string code, string userAgent, string ip);
    Task<string> CreateUrlAsync(CreateUrlRequest request);
    Task<UrlEntity> GetUrlDetailsAsync(string code);
    Task<UrlEntity> UpdateUrlAsync(string code, UpdateUrlRequest request);
    Task<bool> DelateUrlAsync(string code);
}