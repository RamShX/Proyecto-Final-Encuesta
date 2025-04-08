
using Domain.Interfeces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pensistence.JWT
{
    public class JwtRepository : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerarToken(Usuario usuariO)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuariO.Id.ToString()),
                new Claim(ClaimTypes.Name, usuariO.Nombre),
                new Claim(ClaimTypes.Email, usuariO.Email),
                new Claim(ClaimTypes.Role, usuariO.Rol.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(40);

            var TokenDescriptor = new JwtSecurityToken(

                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(TokenDescriptor);

            return Task.FromResult(token);
        }
    }
}
