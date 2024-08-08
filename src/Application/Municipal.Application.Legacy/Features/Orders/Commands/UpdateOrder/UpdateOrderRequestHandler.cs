using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderRequestHandler : EndpointBase.WithRequestWithResult<UpdateOrderRequest, OperationResult<string>>
{
    private readonly IOrdersRepository _ordersRepository;

    public UpdateOrderRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    [HttpPost("api/Orders/UpdateOrder")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/UpdateOrder",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] UpdateOrderRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.UpdateOrder(request, cancellationToken);
        return responce.ToOperationResult();
    }
}