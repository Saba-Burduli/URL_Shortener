using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.InterFaces;
using URL_Shortener.Models;
using URL_Shortener.Utils;

namespace URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
         private readonly ICassandraSession _session;
       

    public UrlsController(ICassandraSessionFactory sessionFactory)
    {
        _session = sessionFactory.GetCassandraSession(); 
        
    }

    // POST api/urls
    [HttpPost]
    public async Task<IActionResult> CreateUrl([FromBody] CreateUrlRequest request)
    {
        var shortCode = request.CustomAlias ?? Base62Encoder.Encode(DateTime.UtcNow.Ticks);
        var url = new UrlEntity
        {
            ShortCode = shortCode,
            OriginalUrl = request.OriginalUrl,
            CreatedAt = DateTime.UtcNow,
            ExpirationDate = request.ExpirationDate,
            ClickCount = 0,
            IsActive = true
        };

        await _session.InsertUrlAsync(url);

        return CreatedAtAction(nameof(GetUrl), new { code = url.ShortCode }, url);
    }

    // GET api/urls/{code}
    [HttpGet("{code}")]
    public async Task<IActionResult> GetUrl(string code)
    {
        var url = await _session.GetUrlAsync(code);
        if (url == null) return NotFound();

        return Ok(url);
    }

    // PUT api/urls/{code}
    [HttpPut("{code}")]
    public async Task<IActionResult> UpdateUrl(string code, [FromBody] UpdateUrlRequest request)
    {
        await _session.UpdateUrlAsync(code, request);
        var updatedUrl = await _session.GetUrlAsync(code);
        if (updatedUrl == null) return NotFound();

        return Ok(updatedUrl);
    }

    // DELETE api/urls/{code}
    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteUrl(string code)
    {
        var success = await _session.DelateUrlAsync(code);
        if (!success) return NotFound();

        return NoContent();
    }

    // GET /{shortCode} - Redirect Endpoint
    [HttpGet("/[controller]/{shortCode}")]
    public async Task<IActionResult> RedirectToOriginalUrl(string shortCode)
    {
        var url = await _session.GetUrlAsync(shortCode);
        if (url == null || !url.IsActive == false)
            return NotFound("URL not found or expired.");

        // Increment clicks and save analytics
        await _session.IncrementClickAsync(shortCode);

        // Optionally: get User-Agent and IP for analytics
        var userAgent = Request.Headers["User-Agent"].ToString();
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        await _session.SaveAnalyticsAsync(shortCode, userAgent, ipAddress);

        return Redirect(url.OriginalUrl);
    }
    }
}
