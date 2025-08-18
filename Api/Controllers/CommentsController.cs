using Application.Comments.Commands.CreateComment;
using Application.Comments.Dtos;
using Application.Comments.Queries.GetComments;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{id}")]
        public async Task<IResult> CreateComment(Guid id, CommentRequestDto commentDto)
        {
            var response = await mediator.Send(new CreateCommentCommand(id, commentDto.Text));
            return Results.Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetComments(Guid id)
        {
            return Results.Ok(await mediator.Send(new GetCommentsQuery(id)));
        }
    }

}
