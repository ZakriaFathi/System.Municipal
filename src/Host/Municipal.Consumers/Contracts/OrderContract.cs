using Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;
using Municipal.Utils.Enums;

namespace Municipal.Consumers.Contracts;

public class OrderContract
{
    public Guid RequestId { get; set; }
    public CreateOrderRequest CreateOrderRequest { get; set; }
    public FormType FormType { get; set; }

}