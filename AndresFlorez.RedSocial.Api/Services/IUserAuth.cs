using AndresFlorez.RedSocial.Api.Models;

namespace AndresFlorez.RedSocial.Api.Services
{
    public interface IUserAuth
    {
        UserAuthResponse Auth(UserAuthRequest model);

        bool ValidateToken(string token);
    }
}
