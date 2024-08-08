using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;

public class CreateOrderRequestHandler : EndpointBase.WithRequestWithResult<CreateOrderRequest, OperationResult<string>>
{
    private readonly IOrdersRepository _ordersRepository;

    public CreateOrderRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    [HttpPost("api/Orders/CreateOrder")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/CreateOrder",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody]CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.CreateOrder(request, cancellationToken);
        return responce.ToOperationResult();
    }
}