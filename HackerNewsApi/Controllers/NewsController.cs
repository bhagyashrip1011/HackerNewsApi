using HackerNewsApi.Interfaces;
using HackerNewsApi.Model;
using HackerNewsApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public NewsController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        /// <summary>
        /// Get News per page
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("newest/{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetNewestStories([FromRoute]int pageNo, [FromRoute] int pageSize)
        {
            HackerNewsResponse result = await _hackerNewsService.GetNewsAsync(pageNo,pageSize);
            return Ok(result);
        }

    }
}
