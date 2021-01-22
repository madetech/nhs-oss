using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;

namespace BloodAndPlasma
{
    public static class JwtTokenFactory
    {
        public static string Create(string secretKey, string iss)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload{
                { "iss", iss },
                { "iat", DateTimeOffset.Now.ToUnixTimeSeconds() }
            };
            var securityToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(securityToken);
        }
    }
}
