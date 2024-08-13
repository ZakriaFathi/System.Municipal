using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Municipal.Application.Identity.Contracts;
using Municipal.Domin.Models;

namespace Municipal.Application.Identity.IdentityValidator;

public class UserCredentialsValidator : IExtensionGrantValidator
    {
        private readonly ILogger<UserCredentialsValidator> _logger;
        private readonly UserManager<AppUser> _userManager;

        public UserCredentialsValidator(ILogger<UserCredentialsValidator> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public string GrantType => IdConstants.GrantType.UserCredentials;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userName = context.Request.Raw.Get(IdConstants.Inputs.UserName);
            var password = context.Request.Raw.Get(IdConstants.Inputs.Password);

            if (!userName.Equals(userName))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogInformation("UserName is incorrect");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                _logger.LogInformation("passeord is empty.");
                return;
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                _logger.LogInformation($"Unable to find user {userName}");
                return;
            }

            if (await _userManager.CheckPasswordAsync(user, password) == false)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
                _logger.LogInformation($"Unable to find user {password}");
                return;
            }

            context.Result = new GrantValidationResult(
                subject: user.Id,
                authenticationMethod: GrantType,
                   customResponse: new Dictionary<string, object> { }
                );
        }
    }