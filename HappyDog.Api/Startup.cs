using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HappyDog.Api.Infrastructure;
using HappyDog.Domain;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Identity;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
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
            jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public IConfiguration Configuration { get; }

        private readonly JsonSerializerSettings jsonSerializerSettings;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //.AddJsonOptions(options=>options.SerializerSettings.DateFormatHandling= Newtonsoft.Json.DateFormatHandling.)

            string conn = Configuration.GetConnectionString("HappyDog");
            services.AddDbContext<HappyDogContext>(option => option.UseSqlServer(conn));
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            services.AddIdentity<User, UserRole>().AddDefaultTokenProviders();
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IRoleStore<UserRole>, RoleStore>();

            #region IoC Service
            // Transient：瞬时（Transient）生命周期服务在它们每次请求时被创建。这一生命周期适合轻量级的，无状态的服务。

            // Scoped：作用域（Scoped）生命周期服务在每次请求被创建一次。

            // Singleton：单例（Singleton）生命周期服务在它们第一次被请求时创建。
            // 或者如果你在 ConfigureServices 运行时指定一个实例
            // 并且每个后续请求将使用相同的实例。如果你的应用程序需要单例行为，
            // 建议让服务容器管理服务的生命周期而不是在自己的类中实现单例模式和管理对象的生命周期。

            //https://stackoverflow.com/questions/38138100/what-is-the-difference-between-services-addtransient-service-addscope-and-servi

            services.AddScoped<ArticleService>();
            services.AddScoped<UserService>();
            #endregion

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = async context =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    string json = JsonConvert.SerializeObject(HttpBaseResult.Unauthorized, jsonSerializerSettings);
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

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                string content = null;
                switch (context.HttpContext.Response.StatusCode)
                {
                    case StatusCodes.Status401Unauthorized:
                        content = JsonConvert.SerializeObject(HttpBaseResult.Unauthorized, jsonSerializerSettings);
                        break;
                    case StatusCodes.Status404NotFound:
                        content = JsonConvert.SerializeObject(HttpBaseResult.NotFound, jsonSerializerSettings);
                        break;
                }
                await context.HttpContext.Response.WriteAsync(content, Encoding.UTF8);
            });

            app.UseAuthentication();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));
            app.UseMvc();
        }
    }
}
