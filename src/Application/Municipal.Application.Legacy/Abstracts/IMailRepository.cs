using FluentResults;
using Municipal.Application.Legacy.Features.Email;

namespace Municipal.Application.Legacy.Abstracts;

public interface IMailRepository
{
    Task<Result<string>> SendEmailAsync(SendEmailRequest request);
}