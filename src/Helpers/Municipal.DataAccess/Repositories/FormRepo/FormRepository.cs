using FluentResults;
using Microsoft.EntityFrameworkCore;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Forms.Commands.CreateForm;
using Municipal.Application.Legacy.Features.Forms.Commands.DeleteForm;
using Municipal.Application.Legacy.Features.Forms.Commands.UpdateForm;
using Municipal.Application.Legacy.Features.Forms.Queries;
using Municipal.Application.Legacy.Features.Forms.Queries.GetAllForms;
using Municipal.Application.Legacy.Features.Forms.Queries.GetFormByFormType;
using Municipal.Application.Legacy.Features.Forms.Queries.GetFormByName;
using Municipal.DataAccess.Persistence;
using Municipal.Domin.Entities;

namespace Municipal.DataAccess.Repositories.FormRepo;

public class FormRepository : IFormRepository
{
    private readonly FormsDbContext _dbContext;
    
    public FormRepository(FormsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetFormResponse>>> GetAll(GetAllFormsRequest request, CancellationToken cancellationToken)
    {
        var form = await _dbContext.Forms
            .Select(x => new GetFormResponse()
            {
                FormId = x.Id,
                Name = x.Name,
                FormType = x.FormType
            }).ToListAsync(cancellationToken);
        return form;
    }

    public async Task<Result<GetFormResponse>> GetFormByName(GetFormByNameRequest request, CancellationToken cancellationToken)
    {
        var form = await _dbContext.Forms.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (form is null)
            return Result.Fail( "هذا النموذج غير موجود" );
        
        var result = new GetFormResponse
        {
            FormId = form.Id,
            Name = form.Name,
            FormType = form.FormType
        };
        
        return result;
    }

    public async Task<Result<string>> CreateForm(CreateFormRequest request, CancellationToken cancellationToken)
    {
        //var validator = new CreateFormRequestValidator();
        //var result1 = validator.Validate(request);
        //if (result1.IsValid == false)
        //    return Result<string>.UnValid(new List<string>() { result1.Errors[0].ErrorMessage });
        
        var form = await _dbContext.Forms.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if(form != null)
            return Result.Fail("هذا النموذج موجود");
        
        var result = new Form()
        {
            Id =  Guid.NewGuid(),
            Name = request.Name,
            FormType = request.FormType,
        };
        
        await _dbContext.Forms.AddAsync(result, cancellationToken); 
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return result.Name;
    }

    public async Task<Result<string>> UpdateForm(UpdeteFormRequest request, CancellationToken cancellationToken)
    { 
        //var validator = new UpdeteFormRequestValidator();
        //var result = validator.Validate(request);
        //if (result.IsValid == false)
        //    return Result<string>.UnValid(new List<string>() { result.Errors[0].ErrorMessage });
        
        var form = await _dbContext.Forms.FirstOrDefaultAsync(x => x.Id == request.FormId, cancellationToken);
        if(form == null)
            return Result.Fail("هذا النموذج غير موجود");
        
        form.Name = request.Name;
        form.FormType = request.FormType;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return "تم التعديل";
    }

    public async Task<Result<string>> DeleteForm(DeleteFormRequest request, CancellationToken cancellationToken)
    {
        var form = await _dbContext.Forms.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (form is null)
            return Result.Fail("هذا النموذج غير موجود");

        _dbContext.Forms.Remove(form);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return "تم الحذف";
    }
    

    public async Task<Result<List<GetFormResponse>>> GetFormsByFormType(GetFormByFormTypeRequest request, CancellationToken cancellationToken)
    {
        var form = await _dbContext.Forms.Where(x => x.FormType == request.FormType)
            .Select(x => new GetFormResponse()
            {
                FormId = x.Id,
                Name = x.Name,
                FormType = x.FormType
            }).ToListAsync(cancellationToken);

        if (form.Count <= 0)
            return Result.Fail("هذا النموذج غير موجود");

        return form;
    }
}