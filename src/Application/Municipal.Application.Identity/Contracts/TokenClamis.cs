using Municipal.Utils.Enums;

namespace Municipal.Application.Identity.Contracts;

public static class TokenClamis
{
    public static List<string> AddClaims()
    {
        var enumValues = Enum.GetValues(typeof(ClaimsTypesInToken));
        var claims = new List<string>();

        foreach (var value in enumValues)
        {
            claims.Add(value.ToString());
        }

        return claims;
    }
}