using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.Forms.Commands.UpdateForm;

public class UpdeteFormRequest 
{
    public Guid FormId { get; set; }
    public string Name { get; set; }
    public FormType FormType { get; set; }
}