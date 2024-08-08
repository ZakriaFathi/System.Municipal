using Municipal.Utils.Enums;

namespace Municipal.Application.Legacy.Features.Forms.Commands.CreateForm;

public class CreateFormRequest 
{
    public string Name { get; set; }
    public FormType FormType { get; set; }
}