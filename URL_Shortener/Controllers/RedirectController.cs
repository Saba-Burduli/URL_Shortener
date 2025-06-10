using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.InterFaces;

namespace URL_Shortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public RedirectController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToOriginal(string code)
        {
            var url = await _urlService.GetUrlDetailsAsync(code);
            if (url == null || !url.IsActive == false || (url.ExpirationDate.HasValue && url.ExpirationDate < DateTime.UtcNow))
            {
                return NotFound("Short URL not found or expired.");
            }

            var userAgent = Request.Headers["User-Agent"].ToString();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            
            //should i add another method ? in here
            await _urlService.RedirectAsync(code, userAgent, ip);

            return Redirect(url.OriginalUrl);
        }
    }
}
