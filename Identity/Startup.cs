using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Hosting;  
using Microsoft.AspNetCore.Identity;  
using Microsoft.EntityFrameworkCore;  
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.Extensions.Hosting;  
using Microsoft.IdentityModel.Tokens;  
using System.Text;
using Identity.Data;
using Identity.Models;
using Identity.Services;
using Identity.Services.Imp;
using Microsoft.OpenApi.Models;

namespace Identity
{
   public class Startup  
    {  
        public Startup(IConfiguration configuration)  
        {  
            Configuration = configuration;  
        }  
  
        public IConfiguration Configuration { get; }  
  
        // This method gets called by the runtime. Use this method to add services to the container.  
        public void ConfigureServices(IServiceCollection services)  
        {  
            services.AddControllers();

            services.AddScoped<IUserManagementService, UserManagementService>();
            
            services.AddAutoMapper(typeof(Startup));
            
            // For Entity Framework  
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));  
  
            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                } )  
                .AddEntityFrameworkStores<ApplicationDbContext>()  
                .AddDefaultTokenProviders();  
  
            // Adding Authentication  
            services.AddAuthentication(options =>  
            {  
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            })  
  
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>  
            {  
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false;  
                options.TokenValidationParameters = new TokenValidationParameters()  
                {  
                    ValidateIssuer = true,  
                    ValidateAudience = true,  
                    ValidAudience = Configuration["JWT:ValidAudience"],  
                    ValidIssuer = Configuration["JWT:ValidIssuer"],  
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))  
                };  
            });  
            
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Awesome API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement  
                {  
                    {  
                        new OpenApiSecurityScheme  
                        {  
                            Reference = new OpenApiReference  
                            {  
                                Type = ReferenceType.SecurityScheme,  
                                Id = "Bearer"  
                            }  
                        },  
                        new string[] {}
                    }  
                });
            });
        }  
  
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  
        {  
            if (env.IsDevelopment())  
            {  
                app.UseDeveloperExceptionPage();  
            }  
  
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Awesome API V1");
            });
            
            app.UseRouting();  
  
            app.UseAuthentication();  
            app.UseAuthorization();  
  
            app.UseEndpoints(endpoints =>  
            {  
                endpoints.MapControllers();  
            });  
        }  
    }  
}
