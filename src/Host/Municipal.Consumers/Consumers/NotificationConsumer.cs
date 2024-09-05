using MassTransit;
using Microsoft.Extensions.Options;
using Municipal.Application.Legacy.Abstracts;
using Municipal.Application.Legacy.Features.Identity.Email;
using Municipal.Consumers.Contracts;

namespace Municipal.Consumers.Consumers;

public class NotificationConsumer : BaseConsumer<NotificationContract>
{
    private readonly IMailRepository _mailRepository;

    public NotificationConsumer(IMailRepository mailRepository, IOptions<RetryOptions> retryOptions, IOrdersRepository ordersRepository) : base(retryOptions, ordersRepository)
    {
        _mailRepository = mailRepository;
    }

    protected override async Task ConsumeMessage(ConsumeContext<NotificationContract> context)
    {
        var message = context.Message;
        var result = await _mailRepository.SendEmailAsync(new SendEmailRequest
        {
            ToEmail = context.Message.ToEmail,
            Subject = "موافقة لطلبية",
            html = "موافقة لطلبية"
        });
    }
}

public class NotificationSent
{ 
    public Guid  RequestId { get; set; }
}