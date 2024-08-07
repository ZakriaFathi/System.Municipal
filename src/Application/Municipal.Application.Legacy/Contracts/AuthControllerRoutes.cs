namespace Municipal.Application.Legacy.Contracts;

public static class AuthControllerRoutes
{
    public const string CreateUser = "CreateUser";
    public const string UpdateUser = "UpdateUser";
    public const string UpdateUserProfile = "UpdateUserProfile";
    public const string UpdateUserKyc = "UpdateUserKyc";
    public const string ChangePassword = "ChangePassword";
    public const string ChangeUserActivation = "ChangeUserActivation";
    public const string GetAllUsers = "GetAllUsers";
    public const string GetUser = "GetUser";

    public const string SingInByUserName = "SingInByUserName";
    public const string SingInByOtp = "SingInByOtp";
    public const string RefreshToken = "RefreshToken";
    public const string ResetPassword = "ResetPassword";
    public const string ForgotPassword = "ForgotPassword";
    public const string SingUp = "SingUp";

    public const string CreateUserClaims = "CreateUserClaims";
    public const string UpdateClaims = "UpdateUserClaims";
    public const string GetUserClaims = "GetUserClaims";
}