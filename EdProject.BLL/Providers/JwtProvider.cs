using EdProject.BLL.Common.Options;
using EdProject.BLL.Providers.Interfaces;
using EdProject.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EdProject.BLL
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        
        public string GenerateAccessToken(User appUser,IList<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim("id", appUser.Id.ToString()),
            };

            foreach(var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = new JwtSecurityToken(_jwtOptions.Issuer,
                                             _jwtOptions.Audience,
                                             claims,
                                             expires: DateTime.Now.AddMinutes(45), 
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNum = new byte[VariableConstant.REFRESH_TOKEN_CAPACITY];
            using(var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNum);
                return Convert.ToBase64String(randomNum);
            }
        }
    }
}
