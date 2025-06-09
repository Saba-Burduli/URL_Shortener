using URL_Shortener.InterFaces;
using URL_Shortener.Models;

namespace URL_Shortener.Services;

public class UrlService : IUrlService
{ 
    public Task<string> RedirectAsync(string code, string userAgent, string ip)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateUrlAsync(CreateUrlRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UrlEntity> GetUrlDetailsAsync(string code)
    {
        throw new NotImplementedException();
    }

    public Task<UrlEntity> UpdateUrlAsync(string code, UpdateUrlRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DelateUrlAsync(string code)
    {
        throw new NotImplementedException();
    }
}