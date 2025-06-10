using Cassandra;
using URL_Shortener.InterFaces;
using URL_Shortener.Models;
using CassandraSession = Cassandra.ISession;
using ISession = Cassandra.ISession;

namespace URL_Shortener.Data;

public class CassandraSession : ICassandraSession
    {
        private readonly ISession _session;
        public CassandraSession(ISession session)
        {
            _session = session;
        }
        
            public async Task<UrlEntity> InsertUrlAsync(UrlEntity url)
            {
                var st = new SimpleStatement(
                    "INSERT INTO urls (shortcode, originalurl, createdat, expirationdate, clickcount, isactive) VALUES (?, ?, ?, ?, ?, ?)");
                var bs = st.Bind(url.ShortCode, url.OriginalUrl, url.CreatedAt, url.ExpirationDate, url.ClickCount, url.IsActive); 
                await _session.ExecuteAsync(bs); // added insise invoked method (SimpleStatment bs) parameter
                return url;
            }

            public async Task<UrlEntity?> GetUrlAsync(string code)
            {
                var st = new SimpleStatement("SELECT * FROM urls WHERE shortcode = ?", code);
                var row = (await _session.ExecuteAsync(st)).FirstOrDefault();
                if (row == null)
                    return null;
                
                return new UrlEntity
                {
                    ShortCode = row.GetValue<string>("shortcode"),
                    OriginalUrl = row.GetValue<string>("originalurl"),
                    CreatedAt = row.GetValue<DateTime>("createdat"),
                    ExpirationDate = row.GetValue<DateTime?>("expirationdate"),
                    ClickCount = row.GetValue<int>("clickcount"),
                    IsActive = row.GetValue<bool>("isactive")
                };
            }
            
        public async Task<UrlEntity> UpdateUrlAsync(string code, UpdateUrlRequest request)
        {
            var updates = new List<string>();
            var values = new List<object>();
            if (request.OriginalUrl != null)
            {
                updates.Add("originalurl = ?");
                values.Add(request.OriginalUrl);
            }
            if (request.ExpirationDate.HasValue)
            {
                updates.Add("expirationdate = ?");
                values.Add(request.ExpirationDate.Value);
            }

            if (updates.Count == 0) 
                throw new InvalidOperationException("No update fields were provided.");
            
            var query = $"UPDATE urls SET {string.Join(", ", updates)} WHERE shortcode = ?";
            values.Add(code);
            var st = new SimpleStatement(query, values.ToArray());
            await _session.ExecuteAsync(st); 
            return await GetUrlAsync(code);
        }

        public async Task<bool> DelateUrlAsync(string code)
        {
            var st = new SimpleStatement("DELATE FROM urls WHERE shortcode = ?", code);
            await _session.ExecuteAsync(st); 
            return true;
        }

        public async Task IncrementClickAsync(string code)
        {
            var st = new SimpleStatement("UPDATE urls SET clickcount = clickcount + 1 WHERE shortcode = ?", code);
            await _session.ExecuteAsync(st);
        }

        public async Task SaveAnalyticsAsync(string code, string userAgent, string ip)
        {
            var stmt = new SimpleStatement(
                "INSERT INTO analytics (shortcode, clickdate, useragent, ipaddress) VALUES (?, toTimestamp(now()), ?, ?)",
                code, userAgent, ip);
            await _session.ExecuteAsync(stmt);
        }
        
        public async Task DeactivateExpiredUrlsAsync() // i added simpleStatment for fix error in line 21
        {
            var stmt = new SimpleStatement("SELECT * FROM urls WHERE isactive = true ALLOW FILTERING");
            var results = await _session.ExecuteAsync(stmt);
            foreach (var row in results)
            {
                var exp = row.GetValue<DateTime?>("expirationdate");
                var code = row.GetValue<string>("shortcode");
                if (exp.HasValue && exp.Value < DateTime.UtcNow)
                {
                    var update = new SimpleStatement("UPDATE urls SET isactive = false WHERE shortcode = ?", code);
                    await _session.ExecuteAsync(update);
                }
            }
        }
}
    
