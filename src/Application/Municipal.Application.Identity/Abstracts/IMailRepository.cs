using FluentResults;
using Municipal.Application.Identity.Features.Email;

namespace Municipal.Application.Identity.Abstracts;

public interface IMailRepository
{
    Task<Result<string>> SendEmailAsync(SendEmailRequest request);
}