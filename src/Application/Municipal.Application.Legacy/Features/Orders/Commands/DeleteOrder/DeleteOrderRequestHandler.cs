using Microsoft.AspNetCore.Mvc;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Utils.Endpoints;
using Municipal.Utils.Vm;
using Swashbuckle.AspNetCore.Annotations;

namespace Municipal.Application.Legacy.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderRequestHandler : EndpointBase.WithRequestWithResult<DeleteOrderRequest, OperationResult<string>>
{
    private readonly IOrdersRepository _ordersRepository;

    public DeleteOrderRequestHandler(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    [HttpDelete("api/Orders/DeleteOrder")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = "Orders/DeleteOrder",
        Tags = new[] { "Orders" }
    )]
    public override async Task<OperationResult<string>> HandleAsync([FromBody] DeleteOrderRequest request, CancellationToken cancellationToken = default)
    {
        var responce = await _ordersRepository.DeleteOrder(request, cancellationToken);
        return responce.ToOperationResult();
    }
}