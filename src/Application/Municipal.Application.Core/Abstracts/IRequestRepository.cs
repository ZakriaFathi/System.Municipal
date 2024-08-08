using FluentResults;
using Municipal.Application.Core.Features.RequestForm;

namespace Municipal.Application.Core.Abstracts;

public interface IRequestRepository
{
    Task<Result<string>> RequestForm(RequestFormCommand request, CancellationToken cancellationToken);

}