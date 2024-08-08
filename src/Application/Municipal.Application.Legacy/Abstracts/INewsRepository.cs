using FluentResults;
using Municipal.Application.Legacy.Features.News.Comments.Commands.CreateComment;
using Municipal.Application.Legacy.Features.News.Comments.Commands.DeleteComment;
using Municipal.Application.Legacy.Features.News.Comments.Commands.UpdateComment;
using Municipal.Application.Legacy.Features.News.Liks.Commands.CreateLike;
using Municipal.Application.Legacy.Features.News.Liks.Commands.DeleteLike;
using Municipal.Application.Legacy.Features.News.Posts.Commands.CreatePost;
using Municipal.Application.Legacy.Features.News.Posts.Commands.DeletePost;
using Municipal.Application.Legacy.Features.News.Posts.Commands.UpdatePost;
using Municipal.Application.Legacy.Features.News.Posts.Queries;
using Municipal.Application.Legacy.Features.News.Posts.Queries.GetAllPosts;

namespace Municipal.Application.Legacy.Abstracts;

public interface INewsRepository
{
    Task<Result<string>> CreatePost(CreatePostRequest request, CancellationToken cancellationToken);
    Task<Result<string>> CreateComment(CreateCommentRequest request, CancellationToken cancellationToken);
    Task<Result<string>> CreateLike(CreateLikeRequest request, CancellationToken cancellationToken);
    
    Task<Result<string>> DeletePost(DeletePostRequest request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteLike(DeleteLikeRequest request, CancellationToken cancellationToken);
    
    Task<Result<string>> UpdatePost(UpdatePostRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateComment(UpdateCommentRequest request, CancellationToken cancellationToken);
    
    Task<Result<List<GetPostsResponse>>> GetAllPosts(GetAllPostsRequest request, CancellationToken cancellationToken);
}