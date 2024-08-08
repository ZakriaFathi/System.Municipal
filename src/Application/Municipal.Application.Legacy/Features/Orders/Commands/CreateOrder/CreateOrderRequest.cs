using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.Orders.Commands.CreateOrder;

public class CreateOrderRequest 
{
    public string UserName { get; set; }
    public OrderState OrderState { get; set; }
    public string FirstName { get; set; }
    public string FatherName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }

}