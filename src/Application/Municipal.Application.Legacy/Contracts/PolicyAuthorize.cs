namespace Municipal.Application.Identity.Contracts;

public static class PolicyAuthorize
{
    //Auth
    public const string SingIn = "SingIn";
    public const string RefreshToken = "RefreshToken";
    public const string ChangePassword = "ChangePassword";
    public const string ResetPassword = "ResetPassword";
    public const string ForgotPassword = "ForgotPassword";
    
    //UserManagement
    public const string GetUsers = "GetUsers";
    public const string CreateUser = "CreateUser";
    public const string ChangeUserActivation = "ChangeUserActivation";
    public const string UpdateUserProfile = "UpdateUserProfile";
    public const string UpdateUserKyc = "UpdateUserKyc";
    public const string UserClaims = "UserClaims";
}