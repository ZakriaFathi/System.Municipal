using IdentityServer4.Validation;

namespace Municipal.Application.Identity.IdentityValidator;

public class RegistrationResponse : ICustomTokenRequestValidator
{
    public async Task ValidateAsync(CustomTokenRequestValidationContext context)
    {
        var c = context;
        var validatedRequest = c.Result.ValidatedRequest;
        var userName = context.Result.ValidatedRequest.Raw.Get("userName");
        var otp = context.Result.ValidatedRequest.Raw.Get("otp");
        //var email = context.Result.ValidatedRequest.Raw.Get("email");

        if (otp == null)
        {
            var response = new Dictionary<string, object>()
            {
                { "userName", userName},
            };
            context.Result.CustomResponse = response;
        }
        else
        {
            var response = new Dictionary<string, object>()
            {
                { "otp", otp},
            };
            context.Result.CustomResponse = response;
        }

    }
}