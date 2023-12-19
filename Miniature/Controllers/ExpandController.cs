using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Miniature.Services.Interfaces;
using System.Net;

namespace Miniature.Controllers
{
    [ApiController]
    [Route("/")]
    public class ExpandController : ControllerBase
    {
        private readonly IShortenService _shortenService;
        public ExpandController(IShortenService shortenService)
        {
            _shortenService = shortenService;
        }

        [HttpGet]
        [Route("{value}")]
        public async Task<IActionResult> GoToUrl(string value)
        {
            try
            {
                string originalUrl = await _shortenService.Expand(value);
            
                return Redirect(originalUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
