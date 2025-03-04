using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UnitedHealth.Server.Config
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IConfiguration _configuration;
        public TokenBuilder(IConfiguration configuration) {
            this._configuration = configuration;
        }

        public string BuildToken(string id, string profile)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim("profile", profile),
                new Claim("id", id)
            };
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
