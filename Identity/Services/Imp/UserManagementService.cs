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
    public class UserManagementService : IUserManagementService
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

        public async Task<RegisterOutputDto> RegisterUser(RegisterInputDto inputDto, string type)
        {
            await AddRolesToRoleManager();
            var userExists = await userManager.FindByNameAsync(inputDto.Username);
            if (userExists != null)
                throw new ValidationException("User already exists!");

            ApplicationUser user = new ApplicationUser()
            {
                Email = inputDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = inputDto.Username
            };
            var result = await userManager.CreateAsync(user, inputDto.Password);
            if (!result.Succeeded)
                throw new ValidationException("User creation failed! Please check user details and try again.");

            switch (type)
            {
                case UserRoles.User:
                {
                    if (await roleManager.RoleExistsAsync(UserRoles.User))
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.User);
                    }

                    break;
                }
                case UserRoles.Writer:
                {
                    if (await roleManager.RoleExistsAsync(UserRoles.Writer))
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.Writer);
                    }

                    break;
                }
                case UserRoles.Admin:
                {
                    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }

                    break;
                }
            }
            return new RegisterOutputDto {Status = "Success", Message = "User created successfully!"};
        }

        private async Task AddRolesToRoleManager()
        {
            Type type = typeof(UserRoles);
            foreach (var p in type.GetFields())
            {
                var v = p.GetValue(null); // static classes cannot be instanced, so use null...
                if (v == null) continue;
                Console.WriteLine(v.ToString());
                if (!await roleManager.RoleExistsAsync(v.ToString()))
                    await roleManager.CreateAsync(new IdentityRole(v.ToString()));
            }
        }
    }
}