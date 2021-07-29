using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Controller.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services.Imp
{
    public class UserManagementService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        
        public UserManagementService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<TokenOutputDto> GenerateJwtToken(LoginInputDto loginInputDto)
        {
            var user = await userManager.FindByNameAsync(loginInputDto.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginInputDto.Password))
                throw new ValidationException("User Unauthorized");
            
            // get user roles
            var userRoles = await userManager.GetRolesAsync(user);
            
            // add claims
            var authClaims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim("email", user.Email),
                new Claim("phone_number", "09127024194"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim("role", userRole)));
            
            // generate token
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
                
            return new TokenOutputDto
            {
                Token= new JwtSecurityTokenHandler().WriteToken(token),
                Type = "Bearer",
                ExpirationTime = token.ValidTo
            };
        }
    }
}