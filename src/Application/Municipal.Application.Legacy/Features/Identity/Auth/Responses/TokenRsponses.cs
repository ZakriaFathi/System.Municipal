namespace Municipal.Application.Legacy.Features.Identity.Auth.Responses;

public class RefreshTokenRsponse
{
    public string Access_token { get; set; } = null;

}
public class AccessTokenRsponse
{
    public string Access_token { get; set; }
    public int Expires_in { get; set; }
    public string Token_type { get; set; }
    public string Refresh_token { get; set; }
    public string Scope { get; set; }
}