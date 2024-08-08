using FluentResults;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
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
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserProfile;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Repositories.NewsRepo;

public class NewsRepository : INewsRepository
{
    private readonly IUserManagmentRepository _userManagmentRepository;
    private readonly NewsDbContext _context ;

    public NewsRepository(IUserManagmentRepository userManagmentRepository, NewsDbContext context)
    {
        _userManagmentRepository = userManagmentRepository;
        _context = context;
    }

    public async Task<Result<string>> CreatePost(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManagmentRepository.GetUserProfileAsync(new GetUserProfileRequset(){UserId = request.UserId}, cancellationToken);
        if (!user.IsSuccess)
            return Result.Fail(user.Errors.ToString());

        var post = new Post
        {
            Description = request.Description, 
            UserName = user.Value.UserName
        };
        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<string>> CreateComment(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManagmentRepository.GetUserProfileAsync(new GetUserProfileRequset(){UserId = request.UserId}, cancellationToken);
        if (!user.IsSuccess)
            return Result.Fail(user.Errors.ToString());

        var comment = new Comment
        {
            Description = request.Description, 
            UserName = user.Value.UserName,
            PostId = request.PostId
        };
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<string>> CreateLike(CreateLikeRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManagmentRepository.GetUserProfileAsync(new GetUserProfileRequset(){UserId = request.UserId}, cancellationToken);
        if (!user.IsSuccess)
            return Result.Fail(user.Errors.ToString());

        var like = new Like
        {
            UserName = user.Value.UserName,
            PostId = request.PostId
        };
        _context.Likes.Add(like);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok();    }

    public async Task<Result<string>> DeletePost(DeletePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken: cancellationToken);
        if(post == null)
            return Result.Fail("هذا المنشور غير موجود");
        
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);
        
        return "تم الحذف";    
    }

    public async Task<Result<string>> DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId, cancellationToken: cancellationToken);
        if(comment == null)
            return Result.Fail("هذا التعليق غير موجود");
        
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);
        
        return "تم الحذف";
        
    }

    public async Task<Result<string>> DeleteLike(DeleteLikeRequest request, CancellationToken cancellationToken)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(x => x.Id == request.LikeId, cancellationToken: cancellationToken);
        if(like == null)
            return Result.Fail("هذا التعليق غير موجود");
        
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync(cancellationToken);
        
        return "تم الحذف";    }

    public async Task<Result<string>> UpdatePost(UpdatePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken: cancellationToken);
        if(post == null)
            return Result.Fail("هذا المنشور غير موجود");
        
        post.Description = request.Description;
        await _context.SaveChangesAsync(cancellationToken);
        
        return "تم التعديل";    
    }

   

    public async Task<Result<string>> UpdateComment(UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId, cancellationToken: cancellationToken);
        if(comment == null)
            return Result.Fail("هذا التعليق غير موجود"); 

        comment.Description = request.Description;
        await _context.SaveChangesAsync(cancellationToken);
        
        return "تم التعديل";
    }

    public async Task<Result<List<GetPostsResponse>>> GetAllPosts(GetAllPostsRequest request, CancellationToken cancellationToken)
    {
        var posts = await _context.Posts
            .Include(x=>x.Comments)
            .Include(y => y.Likes)
            .Select(g => new GetPostsResponse
            {
                Id = g.Id,
                UserName = g.UserName,
                Description = g.Description,
                Comments = g.Comments.Select(m => new CommentResponse()
                {
                    UserName = m.UserName,
                    Description = m.Description
                    
                }).ToList(),
                Likes = g.Likes.Select(m => new LikeResponse()
                {
                    UserName = m.UserName
                }).ToList()
            }).ToListAsync(cancellationToken: cancellationToken);
        
        return posts;    
    }
}