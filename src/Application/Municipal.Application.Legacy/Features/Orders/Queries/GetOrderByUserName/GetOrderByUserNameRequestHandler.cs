using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Domin.Entities;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Queries.GetOrderByUserName;

public class GetOrderByUserNameRequestHandler : EndpointBase.WithRequestWithResult<GetOrderByUserNameRequest, OperationResult<List<Order>>>
{
    private readonly IOrdersRepository _ordersRepository;

    public GetOrderByUserNameRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
    [HttpGet("api/Orders/GetOrderByUserName")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/GetOrderByUserName",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<List<Order>>> HandleAsync([FromQuery] GetOrderByUserNameRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.GetOrdersByUserName(request, cancellationToken);
        return responce.ToOperationResult();
    }
}