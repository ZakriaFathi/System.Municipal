using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersRequestHandler : EndpointBase.WithRequestWithResult<GetAllOrdersRequest, OperationResult<List<GetOrderResponse>>>
{
    private readonly IOrdersRepository _ordersRepository;

    public GetAllOrdersRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    [HttpGet("api/Orders/GetAllOrders")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/GetAllOrders",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<List<GetOrderResponse>>> HandleAsync([FromQuery]GetAllOrdersRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.GetAll(request, cancellationToken);
        return responce.ToOperationResult();
    }
}