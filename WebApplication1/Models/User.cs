using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public String FullName { get; set; }

        public String UserName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }

        public DateTime? AddDate { get; set; }

        public String Address { get; set; }


        public static string GenerateJwtToken(string UserName)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,UserName),
             new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
              new Claim(ClaimTypes.NameIdentifier,UserName)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"])));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(Convert.ToString(ConfigurationManager.AppSettings["config:JwtExpireMinutes"])));

            var token = new JwtSecurityToken(
                Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
                Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
                claims,
                expires: expires,
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ValidateToken(string token)
        {
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));


            try
            {


                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var jti = jwtToken.Claims.First(claim => claim.Type == "jti").Value;
                var userName = jwtToken.Claims.First(sub => sub.Type == "sub").Value;
                return userName;
            }
            catch
            {
                return null;
            }
        

        }
    }
}