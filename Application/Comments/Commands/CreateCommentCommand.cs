using Application.Comments.Dtos;

namespace Application.Comments.Commands
{
    public record CreateCommentCommand(Guid TopicId, string Body) : ICommand<CreateCommentResult>;

    public record CreateCommentResult(CommentDto Result);
}
