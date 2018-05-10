using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Identity;
using HappyDog.Domain.Models.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HappyDog.Api
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
            services.AddMvc();
            //.AddJsonOptions(options=>options.SerializerSettings.DateFormatHandling= Newtonsoft.Json.DateFormatHandling.)

            string conn = Configuration.GetConnectionString("HappyDog");
            services.AddDbContext<HappyDogContext>(option => option.UseSqlServer(conn));
            services.AddAutoMapper();

            services.AddIdentity<User, UserRole>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = async context =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    string json = JsonConvert.SerializeObject(HttpBaseResult.Unauthorized, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    await context.HttpContext.Response.WriteAsync(json, Encoding.UTF8);
                };
            });
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        //if (context.Request.Path.StartsWithSegments("/api"))
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //});

            /*
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // 如果没有设置LoginPath， ASP.NET Core 默认登录路径是 /Account/Login
                options.LoginPath = "/Account/Login";
                // 如果没有设置AccessDeniedPath，ASP.NET Core 默认访问失败路径是 /Account/AccessDenied
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStatusCodePages(async context =>
            //{
            //    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            //    string json = JsonConvert.SerializeObject(HttpBaseResult.Unauthorized);
            //    await context.HttpContext.Response.WriteAsync(json, Encoding.UTF8);
            //});

            app.UseAuthentication();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
            app.UseMvc();
        }
    }
}
