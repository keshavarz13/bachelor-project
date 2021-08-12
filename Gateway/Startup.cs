using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.AspNetCore.Builder;  
using Microsoft.AspNetCore.Hosting;   
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;  
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Gateway
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
            services.AddHttpClient();
            
            // Configure Authentication
            services.AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:ValidAudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                    };
                });
            
            services.AddMvc();
        }  
  
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {  
            if (env.IsDevelopment())  
            {  
                app.UseDeveloperExceptionPage();  
            }

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
