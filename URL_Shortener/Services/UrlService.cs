using URL_Shortener.InterFaces;
using URL_Shortener.Models;
using URL_Shortener.Utils;

namespace URL_Shortener.Services;

public class UrlService : IUrlService
{
    private readonly ICassandraSessionFactory _sessionFactory;

    public UrlService(ICassandraSessionFactory sessionFactory)
    {
        _sessionFactory = sessionFactory;
    }
    public async Task<string> RedirectAsync(string shortcode, string userAgent, string ip)
    {
        var session = _sessionFactory.GetCassandraSession();
        var url = await session.GetUrlAsync(shortcode);
        if (url == null || url.IsActive != true || (url.ExpirationDate != null))
        {
            return null;
        }

        await session.IncrementClickAsync(shortcode);
        await session.SaveAnalyticsAsync(shortcode, userAgent, ip);
        return url.OriginalUrl;
    }

    public async Task<string> CreateUrlAsync(CreateUrlRequest request)
    {
        var shortcode = request.CustomAlias ?? Base62Encoder.Encode(DateTime.UtcNow.Ticks);
        var url = new UrlEntity()
        {
            ShortCode = shortcode,
            OriginalUrl = request.OriginalUrl,
            CreatedAt = DateTime.UtcNow,
            ExpirationDate = request.ExpirationDate,
            ClickCount = 0,
            IsActive = true
        };
        await _sessionFactory.GetCassandraSession().InsertUrlAsync(url);
        return shortcode; //check if this is right or use url . But using url in return gives me this error:
                          //Cannot convert expression type 'URL_Shortener.Models.UrlEntity' to return type 'string' 
    }

    public async Task<UrlEntity> GetUrlDetailsAsync(string code)
    {
        return await _sessionFactory.GetCassandraSession().GetUrlAsync(code);
    }

    public async Task<UrlEntity> UpdateUrlAsync(string code, UpdateUrlRequest request)
    {
        return await _sessionFactory.GetCassandraSession().UpdateUrlAsync(code,request);
    }

    public async Task<bool> DelateUrlAsync(string code)
    {
        return await _sessionFactory.GetCassandraSession().DelateUrlAsync(code);
    }
}