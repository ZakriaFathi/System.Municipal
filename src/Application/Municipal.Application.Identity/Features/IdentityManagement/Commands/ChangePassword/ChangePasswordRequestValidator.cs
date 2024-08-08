using FluentValidation;

namespace Municipal.Application.Identity.Features.IdentityManagement.Commands.ChangePassword;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(p => p.UserId)
            .NotEmpty().WithMessage("يرجي إدخال رمز المستخدم.");


        RuleFor(p => p.OldPassword)
            .NotEmpty().WithMessage("يرجي إدخال كلمة المرور.")
            .NotNull()
            .Matches("^[a-zA-Z0-9!@#$%^&*()_-]+$").WithMessage("يجب أن تحنوي كلمة المرور على ارقام و حروف و رموز.")
            .MaximumLength(8 - 16).WithMessage("يجب ان تكون كلمة المرور من 8 الى 16.");


        RuleFor(p => p.NewPassword)
            .NotEmpty().WithMessage("يرجي إدخال كلمة المرور.")
            .NotNull()
            .Matches("^[a-zA-Z0-9!@#$%^&*()_-]+$").WithMessage("يجب أن تحنوي كلمة المرور على ارقام و حروف و رموز.")
            .MaximumLength(8 - 16).WithMessage("يجب ان تكون كلمة المرور من 8 الى 16.");
    }
}