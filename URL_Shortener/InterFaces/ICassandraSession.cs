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

public class CassandraSession : ICassandraSession
{
    private readonly ISession _session;
    public CassandraSession(ISession session)
    {
        _session = session;
    }
    public Task<UrlEntity> InsertUrlAsync(UrlEntity url)
    {
        throw new NotImplementedException();
    }

    public Task<UrlEntity> GetUrlAsync(string code)
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

    public Task IncrementClickAsync(string code)
    {
        throw new NotImplementedException();
    }

    public Task SaveAnalyticsAsync(string code, string userAgent, string ip)
    {
        throw new NotImplementedException();
    }

    public Task DeactivateExpiredUrlsAsync()
    {
        throw new NotImplementedException();
    }
}