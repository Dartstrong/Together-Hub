using Application.Topics;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(ITopicsService topicsService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Topic>>> GetTopics()
        {
            return Ok(await topicsService.GetTopicsAsync());
        }
    }
}
