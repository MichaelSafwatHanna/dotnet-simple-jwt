using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OAuth.Api.Dtos;
using OAuth.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtConfiguration jwtConfiguration;

        public AuthenticationController(IOptions<JwtConfiguration> jwtConfiguration)
        {
            this.jwtConfiguration = jwtConfiguration.Value;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, "user_id_value"),
                new Claim(ClaimTypes.Name, dto.Username),
                new Claim(ClaimTypes.Email, dto.Username),
                new Claim(ClaimTypes.GivenName, dto.Username),
                new Claim(ClaimTypes.Role, "administrator")
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: jwtConfiguration.Issuer,
                expires: DateTime.Now.AddMinutes(jwtConfiguration.ExpiredInMinutes),
                claims: claims,
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new JwtTokenResponse { Token = tokenString });
        }
    }
}
