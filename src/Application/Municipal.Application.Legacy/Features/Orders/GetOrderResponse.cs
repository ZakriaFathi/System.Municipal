using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.Orders;

public class GetOrderResponse
{
    public Guid Id { get; set; }
    public OrderState OrderState { get; set; }
    public string UserName { get; set; }
}