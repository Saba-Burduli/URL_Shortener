namespace URL_Shortener.InterFaces;

public interface ICassandraSessionFactory
{
    ICassandraSession GetCassandraSession();
}