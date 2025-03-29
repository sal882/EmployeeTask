using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EmployeeTask.VerticalSlicing.Data.Entities;

namespace EmployeeTask.VerticalSlicing.Features.Auth.Common
{
    public class TokenGenerator
    {
        public static JwtOptions options { get; set; } = null!;
        public static string GenerateToken(User user)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
            });

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(options.ExpiryMinutes),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public static RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
