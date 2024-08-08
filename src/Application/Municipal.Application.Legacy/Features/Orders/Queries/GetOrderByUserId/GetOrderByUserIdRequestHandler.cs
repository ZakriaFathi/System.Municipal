using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Queries.GetOrderByUserId;

public class GetOrderByUserIdRequestHandler : EndpointBase.WithRequestWithResult<GetOrderByUserIdRequest, OperationResult<GetOrderResponse>>
{
    private readonly IOrdersRepository _ordersRepository;

    public GetOrderByUserIdRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    [HttpGet("api/Orders/GetOrderByUserId")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/GetOrderByUserId",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<GetOrderResponse>> HandleAsync([FromQuery]GetOrderByUserIdRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.GetOrderByUserId(request, cancellationToken);
        return responce.ToOperationResult();
    }
}