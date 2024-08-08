using Municipal.Utils.Enums;

namespace Municipal.Application.Core.Features.RequestForm;

public class RequestFormCommand
{
    public Guid UserId { get; set; }
    public FormType FormType { get; set; }
}