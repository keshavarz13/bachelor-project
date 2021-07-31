using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Controller.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services.Imp
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        
        public UserManagementService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IMapper mapper)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<TokenOutputDto> GenerateJwtToken(LoginInputDto loginInputDto)
        {
            var user = await _userManager.FindByNameAsync(loginInputDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginInputDto.Password))
                throw new ValidationException("User Unauthorized");
            
            // get user roles
            var userRoles = await _userManager.GetRolesAsync(user);
            
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
            var userExists = await _userManager.FindByNameAsync(inputDto.Username);
            if (userExists != null)
                throw new ValidationException("User already exists!");

            ApplicationUser user = new ApplicationUser()
            {
                Email = inputDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = inputDto.Username,
                PhoneNumber = inputDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, inputDto.Password);
            if (!result.Succeeded)
                throw new ValidationException("User creation failed! Please check user details and try again.");

            switch (type)
            {
                case UserRoles.User:
                {
                    if (await _roleManager.RoleExistsAsync(UserRoles.User))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                    }

                    break;
                }
                case UserRoles.Writer:
                {
                    if (await _roleManager.RoleExistsAsync(UserRoles.Writer))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Writer);
                    }

                    break;
                }
                case UserRoles.Admin:
                {
                    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }

                    break;
                }
            }
            return new RegisterOutputDto {Status = "Success", Message = "User created successfully!"};
        }

        public async Task<List<UserReportOutputDto>> GetUsers()
        {
            return _mapper.Map<List<UserReportOutputDto>>(await _userManager.Users.ToListAsync());
        }

        public async Task<IdentityResult> RemoveUser(string username)
        {
            return await _userManager.DeleteAsync(await _userManager.FindByNameAsync(username));
        }

        public async Task<List<UserReportOutputDto>> GetUserByPhoneNumber(string phoneNumber)
        {
            return _mapper.Map<List<UserReportOutputDto>>(await _userManager.Users.AsQueryable().Where(x => x.PhoneNumber == phoneNumber).ToListAsync());
        }
        
        public async Task<List<UserReportOutputDto>> GetUserByEmail(string email)
        {
            return _mapper.Map<List<UserReportOutputDto>>(await _userManager.Users.AsQueryable().Where(x => x.Email == email).ToListAsync());
        }
        
        private async Task AddRolesToRoleManager()
        {
            Type type = typeof(UserRoles);
            foreach (var p in type.GetFields())
            {
                var v = p.GetValue(null); 
                if (v == null) continue;
                if (!await _roleManager.RoleExistsAsync(v.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole(v.ToString()));
            }
        }
    }
}