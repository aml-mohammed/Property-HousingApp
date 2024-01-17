using Housing.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Housing.API.Services
{
    public static class CreateToken
    {
     //   private readonly IConfiguration _configuration;
       

        //public CreateToken(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public static string CreateJWT(User user, string configkey)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configkey));
         //   var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var singingCredintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = singingCredintials,
              
            };
            var tokenHandeler = new JwtSecurityTokenHandler();
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            return tokenHandeler.WriteToken(token);

        }
    }
}
