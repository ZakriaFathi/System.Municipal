namespace Municipal.Application.Legacy.Contracts;

public class IdConstants 
{
    public class Clients
    {
        public const string Web = "Web";
        public const string Mobile = "Mobile";
    }
    public class Scopes
    {
        public const string WebScope = "Web";
        public const string OtpScope = "Otp";
        public const string UserName = "userName";
    }

    public class Inputs
    {
        public const string UserName = "userName";
        public const string Password = "Password";
        public const string Otp = "otp";

    }
    public class GrantType
    {
        public const string Otp = "otp";
        public const string UserCredentials = "user_credentials";
    } 
        
}