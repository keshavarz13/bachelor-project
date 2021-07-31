using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Hosting;  
using Microsoft.EntityFrameworkCore;  
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.Extensions.Hosting;  
using Microsoft.OpenApi.Models;
using Notification.Data;

namespace Notification
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

            services.AddAutoMapper(typeof(Startup));
            
            // For Entity Framework  
            services.AddDbContext<NotificationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));  
  
            
            // Adding Authentication  
            services.AddAuthentication(options =>  
            {  
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            })  
            .AddJwtBearer(options =>  
            {  
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false;
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
