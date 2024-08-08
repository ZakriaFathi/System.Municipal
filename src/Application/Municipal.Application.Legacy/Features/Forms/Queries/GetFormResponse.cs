using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.Forms.Queries;

public class GetFormResponse
{
    public Guid FormId { get; set; }
    public string Name { get; set; }
    public FormType FormType { get; set; }
}