using Library_Management_API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Helpers
{
    public class AuthServices
    {
        private readonly IConfiguration configuration;

        public AuthServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            try
            {
                var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                };

                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var item in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, item));
                }

                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT")["key"]));
                var authAudience = configuration.GetSection("JWT")["Audience"];
                var authIssuer = configuration.GetSection("JWT")["Issuer"];

                var token = new JwtSecurityToken(
                    audience: authAudience,
                    issuer: authIssuer,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature),
                   expires: DateTime.UtcNow.AddDays(1)
                    );
                Log.Information("token has been created");
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while creating token: {ex.Message}");
                throw new Exception("An error occurred while creating token", ex);
            }

        }
    }
}
