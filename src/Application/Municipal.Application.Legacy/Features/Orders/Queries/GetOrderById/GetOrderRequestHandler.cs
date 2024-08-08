using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Queries.GetOrderById;

public class GetOrderRequestHandler : EndpointBase.WithRequestWithResult<GetOrderRequest, OperationResult<GetOrderResponse>>
{
    private readonly IOrdersRepository _ordersRepository;

    public GetOrderRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
    [HttpGet("api/Orders/GetOrderById")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/GetOrderById",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<GetOrderResponse>> HandleAsync([FromQuery]GetOrderRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.GetById(request, cancellationToken);
        return responce.ToOperationResult();
    }
}