using Microsoft.AspNetCore.Mvc;
using Miniature.Models;
using Miniature.Services.Interfaces;

namespace Miniature.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ShortnerController : ControllerBase
    {
        private IShortenService _shortener;
        public ShortnerController(IShortenService shortnerService) 
        {
            _shortener = shortnerService;
        }

        [HttpPost]
        [Route("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlShortnerDTO urlShortnerDTO)
        {
            string url = await _shortener.Shorten(urlShortnerDTO.Url);
            return Ok(url);
        }
    }
}
