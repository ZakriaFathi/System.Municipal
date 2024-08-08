using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Municipal.Application.Identity.Contracts;
using Municipal.Domin.Models;

namespace Municipal.Application.Identity.IdentityValidator;

public class OtpValidator : IExtensionGrantValidator
{
    private readonly ILogger<OtpValidator> _logger;
    private readonly UserManager<AppUser> _userManager;

    public OtpValidator(ILogger<OtpValidator> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public string GrantType => IdConstants.GrantType.Otp;

    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var otp = context.Request.Raw.Get(IdConstants.Inputs.Otp);
        var userName = context.Request.Raw.Get(IdConstants.Inputs.UserName);


        if (string.IsNullOrEmpty(otp))
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            _logger.LogInformation("code is incorrect");
            return;
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
            _logger.LogInformation($"Unable to find user {userName}");
            return;
        }

        context.Result = new GrantValidationResult(
            subject: user.Id,
            authenticationMethod: GrantType,
            customResponse: new Dictionary<string, object> { }
        );
    }
}