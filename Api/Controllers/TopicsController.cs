using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(ITopicsService topicsService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TopicResponseDto>>> GetTopics(CancellationToken cancellationToken)
        {
            return Ok(await topicsService.GetTopicsAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicResponseDto>> GetTopic(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await topicsService.GetTopicAsync(id, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<TopicResponseDto>> CreateTopic(CreateTopicDto createTopicDto, CancellationToken cancellationToken)
        {
            return Ok(await topicsService.CreateTopicAsync(createTopicDto, cancellationToken));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid id, [FromBody]UpdateTopicDto updateTopicDto, CancellationToken cancellationToken)
        {
            return Ok(await topicsService.UpdateTopicAsync(id, updateTopicDto, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TopicResponseDto>> DeleteTopic(Guid id, CancellationToken cancellationToken)
        {
            await topicsService.DeleteTopicAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
