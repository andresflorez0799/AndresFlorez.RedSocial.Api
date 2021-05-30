using AndresFlorez.RedSocial.Api.Models;
using AndresFlorez.RedSocial.Modelo.EF;
using AndresFlorez.RedSocial.Utilidades;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AndresFlorez.RedSocial.Api.Services
{
    public class UserAuth : IUserAuth
    {
        private readonly AppSettings _appSettings;
        public UserAuth(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        public UserAuthResponse Auth(UserAuthRequest model)
        {
            Logica.Contrato.IUsuarioBl usuarioBl = new Logica.Implementacion.UsuarioBl();
            RsocialUsuario usuario = usuarioBl.GetByEmail(model.Email);

            if (usuario != null && !string.IsNullOrEmpty(usuario.Email)) 
            {
                if (usuario.Contrasena == Crypto.EncriptarAES(model.Password)) 
                {
                    return new UserAuthResponse()
                    {
                        Id = usuario.Id,
                        Token = GetToken(usuario),
                        Email = model.Email,
                        Name = $"{usuario.Nombre} {usuario.Apellido}",
                    };
                }
            }
            return new UserAuthResponse();
        }

        private string GetToken(RsocialUsuario model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("Id", model.Id.ToString()),
                        new Claim("Correo", model.Email.ToString()),
                        new Claim("NombreCompleto", $"{model.Nombre} {model.Apellido}"),
                        new Claim("Telefono", model.Telefono)
                    }),
                Expires = DateTime.UtcNow.AddHours(1), /// solo habilitado para una hora
                Issuer = this._appSettings.JWTIssuer,
                Audience = this._appSettings.JWTAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            var llave = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = this._appSettings.JWTIssuer,
                    ValidAudience = this._appSettings.JWTAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(llave)
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
