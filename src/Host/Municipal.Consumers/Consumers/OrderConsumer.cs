using MassTransit;
using Microsoft.Extensions.Options;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Consumers.Contracts;
using Municipal.Consumers.Saga;
using Municipal.Utils.Vm;

namespace Municipal.Consumers.Consumers;

public class OrderConsumer :
    BaseConsumer<OrderContract>
{
    public OrderConsumer(IOptions<RetryOptions> retryOptions, IOrdersRepository ordersRepository) : base(retryOptions, ordersRepository)
    {
    }

    protected override async Task ConsumeMessage(ConsumeContext<OrderContract> context)
    {
        var message = context.Message;

        var result =
            await OrdersRepository.CreateOrder(message.CreateOrderRequest, cancellationToken: CancellationToken.None);


        if (!result.IsSuccess)
        {
            await context.Publish(new ServiceFailed
            {
                RequestId = message.RequestId,
                ResponseMessage = result.Errors[0].ToString()

            });

            return;

        }

        await context.Publish(new ServiceActivated
        {
            RequestId = message.RequestId,
            Email = message.CreateOrderRequest.EmailAddress,
            ResponseMessage = "Success",
        });

    }
}