using FluentResults;
using Municipal.Application.Legacy.Features.Identity.Email;

namespace Municipal.Application.Legacy.Abstracts;

public interface IMailRepository
{
    Task<Result<string>> SendEmailAsync(SendEmailRequest request);
}