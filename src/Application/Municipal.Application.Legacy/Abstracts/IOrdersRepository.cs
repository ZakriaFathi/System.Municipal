using FluentResults;
using Municipal.Application.Legacy.Features.Orders;
using Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;
using Municipal.Application.Legacy.Features.Orders.Commands.DeleteOrder;
using Municipal.Application.Legacy.Features.Orders.Commands.UpdateOrder;
using Municipal.Application.Legacy.Features.Orders.Queries.GetAllOrders;
using Municipal.Application.Legacy.Features.Orders.Queries.GetOrderById;
using Municipal.Application.Legacy.Features.Orders.Queries.GetOrderByUserId;
using Municipal.Application.Legacy.Features.Orders.Queries.GetOrderByUserName;
using Municipal.Domin.Entities;

namespace Municipal.Application.Legacy.Abstracts;

public interface IOrdersRepository
{
    Task<Result<List<GetOrderResponse>>> GetAll(GetAllOrdersRequest request, CancellationToken cancellationToken);
    Task<Result<GetOrderResponse>> GetById(GetOrderRequest request, CancellationToken cancellationToken);
    Task<Result<List<Order>>> GetOrdersByUserName(GetOrderByUserNameRequest request, CancellationToken cancellationToken);
    Task<Result<GetOrderResponse>> GetOrderByUserId(GetOrderByUserIdRequest request, CancellationToken cancellationToken);
    Task<Result<string>> CreateOrder(CreateOrderRequest request, CancellationToken cancellationToken);
    Task<Result<string>> UpdateOrder(UpdateOrderRequest request, CancellationToken cancellationToken);
    Task<Result<string>> DeleteOrder(DeleteOrderRequest request, CancellationToken cancellationToken);
}