using AndresFlorez.RedSocial.Api.Models;
using AndresFlorez.RedSocial.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AndresFlorez.RedSocial.Api.Controllers
{
    public class CustomControllerBase : ControllerBase
    {

        protected ApiResponse<T> GetResponseApi<T>(T data, int code, bool state)
        {
            var response = new ApiResponse<T>
            {
                RequestCode = code,
                RequestState = state,
                RequestData = data
            };
            return response;
        }

        protected IEnumerable<System.Security.Claims.Claim> GetClaimsToken()
        {
            var identity = HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity;
            return identity.Claims;
        }
        protected ClaimsUserAuth GetClaimsAuthToken()
        {
            IEnumerable<System.Security.Claims.Claim> claims = GetClaimsToken();
            ClaimsUserAuth authClaim = new ClaimsUserAuth();
            authClaim.Id = claims.Where(x => x.Type == "Id").Select(y => y.Value).FirstOrDefault().ToInt();
            authClaim.Correo = claims.Where(x => x.Type == "Correo").Select(y => y.Value).FirstOrDefault().ToString();
            authClaim.NombreCompleto = claims.Where(x => x.Type == "NombreCompleto").Select(y => y.Value).FirstOrDefault().ToString();
            authClaim.Telefono = claims.Where(x => x.Type == "Telefono").Select(y => y.Value).FirstOrDefault().ToString();
            return authClaim;
        }
    }
}
