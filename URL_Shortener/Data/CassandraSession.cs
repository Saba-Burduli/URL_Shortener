using Cassandra;
using URL_Shortener.InterFaces;
using URL_Shortener.Models;
using CassandraSession = Cassandra.ISession;

namespace URL_Shortener.Data;

public class CassandraSession : ICassandraSession
    {
        private readonly CassandraSession _session;
        public CassandraSession(CassandraSession session)
        {
            _session = session;
        }
        
            public async Task<UrlEntity> InsertUrlAsync(UrlEntity url)
            {
                var st = new SimpleStatement(
                    "INSERT INTO urls (shortcode, originalurl, createdat, expirationdate, clickcount, isactive) VALUES (?, ?, ?, ?, ?, ?)");
                var bs = st.Bind(url.ShortCode, url.OriginalUrl, url.CreatedAt, url.ExpirationDate, url.ClickCount, url.IsActive); 
                await _session.DeactivateExpiredUrlsAsync(bs); // added insise invoked method (SimpleStatment bs) parameter
                return url;
            }

        public async Task<UrlEntity> GetUrlAsync(string code)
        {
            var st = new SimpleStatement("SELECT * FROM urls WHERE shortcode = ?", code);
            var row = (await _session.DeactivateExpiredUrlsAsync(st)).FirstOrDefaultAsync();
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

        public Task DeactivateExpiredUrlsAsync(SimpleStatement bs) // i added simpleStatment for fix error in line 21
        {
            throw new NotImplementedException();
        }
    }
