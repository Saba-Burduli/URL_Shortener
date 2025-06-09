using Cassandra;
using URL_Shortener.InterFaces;
using ISession = Microsoft.AspNetCore.Http.ISession;

namespace URL_Shortener.Data;

public class CassandraSessionFactory : ICassandraSessionFactory
{
    private readonly ISession _session;
    public CassandraSessionFactory()
    {
        var cluster = Cluster.Builder()
            .AddContactPoint("cassandra")
            .WithPort(9042)
            .Build();
        _session = cluster.Connect("urlshortener"); // error in there:
                                                    // Cannot convert source type 'Cassandra.ISession' to target type 'Microsoft.AspNetCore.Http.ISession'
    }
    public ICassandraSession GetCassandraSession() => new CassandraSession(_session);
}