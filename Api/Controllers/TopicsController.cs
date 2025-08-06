using Application.Topics.Commands.UpdateTopic;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetTopics(CancellationToken cancellationToken)
        {
            return Results.Ok(await mediator.Send(new GetTopicsQuery(cancellationToken), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetTopic(Guid id, CancellationToken cancellationToken)
        {
            return Results.Ok(await mediator.Send(new GetTopicQuery(id, cancellationToken), cancellationToken));
        }

        [HttpPost]
        public async Task<IResult> CreateTopic([FromBody]CreateTopicDto createTopicDto, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new CreateTopicCommand(createTopicDto, cancellationToken), cancellationToken);
            return Results.Created($"/topics/{response.TopicResponseDto.Id}", response.TopicResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IResult> UpdateTopic(Guid id, [FromBody]UpdateTopicDto updateTopicDto, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new UpdateTopicCommand(id, updateTopicDto, cancellationToken), cancellationToken);
            return Results.Ok(response.TopicResponseDto);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteTopic(Guid id, CancellationToken cancellationToken)
        {
            return Results.Ok(await mediator.Send(new DeleteTopicCommand(id, cancellationToken), cancellationToken));
        }
    }
}
