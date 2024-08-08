// namespace Municipal.Application.Identity.Features.Email;

//public class SendEmailRequestHandler : IRequestHandler<SendEmailRequest, Result<string>>
//{
//    private readonly IMailRepository _mailRepository;

//    public SendEmailRequestHandler(IMailRepository mailRepository)
//    {
//        _mailRepository = mailRepository;
//    }

//    public async Task<Result<string>> Handle(SendEmailRequest request, CancellationToken cancellationToken)
//        => await _mailRepository.SendEmailAsync(request);
//}