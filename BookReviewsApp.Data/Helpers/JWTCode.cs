using BookReviewsApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BookReviewsApp.Data.Helpers
{
    public class JWTCode
    {
        private readonly string secretkey = "JvdO3pCvP0LeW7Q8vzhwA/kN+ifzI4dQPw8ZYCdXOKA="; // Should be hidden on configuration
        private readonly User user;
        public JWTCode(User user) { 
            this.user = user;
        }

        public string Encode()
        {
            var userClaims = new List<Claim>
            {
                new Claim("id", user.id.ToString()),
                new Claim("email", user.email),
                new Claim("firstName", user.firstName),
                new Claim("lastName", user.lastName),
                new Claim("photo", user.photo),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(secretkey);
            Console.Write(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "WebPage",
                Issuer = "bookReview server AZ",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Console.WriteLine(token);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool Verify(string token)
        {
            var hanlder = new JwtSecurityTokenHandler();
            var jsonToken = hanlder.ReadToken(token) as JwtSecurityToken;
            if (jsonToken == null) return false;
            var id = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "id");
            if (id == null) return false;
            else return true;
        }
    }
}
