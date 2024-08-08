namespace Municipal.Application.Legacy.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderRequest 
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string FatherName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
}