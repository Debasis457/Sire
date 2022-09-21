using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Sire.Web.Helpers;
using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sire.Web
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
            /*//Dharini : For upload pdf
            services.AddSingleton<IFileProvider>(
          new PhysicalFileProvider(
              Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));*/

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:key"])),
                    ValidAudience = Configuration["JwtSettings:audience"],
                    ValidIssuer = Configuration["JwtSettings:issuer"]
                };
            });

            services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperConfiguration)));
            services.AddControllersWithViews();
            //Dharini
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                services
               .AddControllersWithViews()
               .AddJsonOptions(options =>
                   options.JsonSerializerOptions.PropertyNamingPolicy = null);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Dharini
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    //context.Response.Redirect("/dashboard");
                }
                else
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseStatusCodePages(context => {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Login");
                return Task.CompletedTask;
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
