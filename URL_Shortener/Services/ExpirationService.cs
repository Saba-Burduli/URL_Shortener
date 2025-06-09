using URL_Shortener.InterFaces;

namespace URL_Shortener.Services;

public class ExpirationService : BackgroundService
{
    private readonly ICassandraSessionFactory _factory;

    public ExpirationService(ICassandraSessionFactory factory)
    {
        _factory = factory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            
        }
    }
}