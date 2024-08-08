using FluentResults;
using MassTransit;
using Municipal.Application.Core.Abstracts;
using Municipal.Application.Core.Features.RequestForm;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Forms.Queries.GetFormByFormType;
using Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserKyc;
using Municipal.Application.Legacy.Features.UserManagement.Users.Queries.GetUserProfile;
using Municipal.Consumers.Saga;
using Municipal.Utils.Enums;

namespace Municipal.DataAccess.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly IUserManagmentRepository _userManagmentRepository;
    private readonly IFormRepository _formRepository;
    private readonly IPublishEndpoint _publishEndpoint;


    public RequestRepository(IUserManagmentRepository userManagmentRepository, IFormRepository formRepository, IPublishEndpoint publishEndpoint)
    {
        _userManagmentRepository = userManagmentRepository;
        _formRepository = formRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result<string>> RequestForm(RequestFormCommand request, CancellationToken cancellationToken)
    {
        var forms = await _formRepository.GetFormsByFormType(new GetFormByFormTypeRequest(){ FormType = request.FormType }, cancellationToken: cancellationToken).ConfigureAwait(false);
        if (forms.Value.Count <= 0)
            return Result.Fail("هذا النموذج غير موجود");

        var user = await _userManagmentRepository.GetUserProfileAsync(new GetUserProfileRequset() { UserId = request.UserId },
            cancellationToken);

        var userKyc = await _userManagmentRepository.GetUserKycAsync(new GetUserKycRequset() { UserId = user.Value.Id },
                cancellationToken);
        
        await _publishEndpoint.Publish(new ServiceDispatched
        {
            RequestId = Guid.NewGuid(),
            FormType = request.FormType,
            Customer = new CreateOrderRequest()
            {
                UserName = user.Value.UserName,
                Address = user.Value.Address,
                EmailAddress = user.Value.Email,
                Country = userKyc.Value.Country,
                FirstName = userKyc.Value.FirstName,
                LastName = userKyc.Value.LastName,
                FatherName = userKyc.Value.FatherName,
                OrderState = OrderState.Pending
            },
        }, cancellationToken);


        return "تم ارسال الطلب";
        
    }
}