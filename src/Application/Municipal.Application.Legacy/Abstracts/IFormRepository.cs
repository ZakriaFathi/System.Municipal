using FluentResults;
using Municipal.Application.Legacy.Features.Forms.Commands.CreateForm;
using Municipal.Application.Legacy.Features.Forms.Commands.DeleteForm;
using Municipal.Application.Legacy.Features.Forms.Commands.UpdateForm;
using Municipal.Application.Legacy.Features.Forms.Queries;
using Municipal.Application.Legacy.Features.Forms.Queries.GetAllForms;
using Municipal.Application.Legacy.Features.Forms.Queries.GetFormByFormType;
using Municipal.Application.Legacy.Features.Forms.Queries.GetFormByName;

namespace Municipal.Application.Legacy.Abstracts;

public interface IFormRepository
{
    Task<Result<List<GetFormResponse>>> GetAll(GetAllFormsRequest request, CancellationToken cancellationToken);
    Task<Result<GetFormResponse>> GetFormByName(GetFormByNameRequest request, CancellationToken cancellationToken);
    Task<Result<List<GetFormResponse>>> GetFormsByFormType(GetFormByFormTypeRequest request, CancellationToken cancellationToken);
    Task<Result<string>> CreateForm(CreateFormRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateForm(UpdeteFormRequest request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteForm(DeleteFormRequest request, CancellationToken cancellationToken);
    
}