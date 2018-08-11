using System;
using System.Text;
using AutoMapper;
using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Identity;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddDbContext<HappyDogContext>(option => option.UseSqlServer(conn));
            services.AddDbContext<HappyDogContext>(option => option.UseSqlite(conn));
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            services.AddIdentity<User, Role>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 9;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/User/SignIn";
                options.AccessDeniedPath = "/User/AccessDenied";
                options.SlidingExpiration = true;
            });

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            //{
            //    options.SlidingExpiration = true;
            //    options.Cookie.HttpOnly = false;
            //    options.Cookie.Domain = Configuration["CookieDomain"];
            //    options.Events.OnRedirectToLogin = async context =>
            //    {
            //        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            //        string json = JsonConvert.SerializeObject(HttpBaseResult.Unauthorized, jsonSerializerSettings);
            //        await context.HttpContext.Response.WriteAsync(json, Encoding.UTF8);
            //    };
            //});

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else if (env.IsProduction())
            //{
            //    if (bool.Parse(Configuration["ShowError"]))
            //    {
            //        app.UseExceptionHandler(options =>
            //        {
            //            options.Run(async context =>
            //            {
            //                var ex = context.Features.Get<IExceptionHandlerFeature>();
            //                if (ex != null)
            //                {
            //                    await context.Response.WriteAsync(ex.Error.ToString());
            //                }
            //            });
            //        });
            //    }
            //}

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200", "https://angular.doghappy.wang")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                HttpBaseResult result = null;
                switch (context.HttpContext.Response.StatusCode)
                {
                    case StatusCodes.Status401Unauthorized:
                        result = HttpBaseResult.Unauthorized;
                        break;
                    case StatusCodes.Status404NotFound:
                        result = HttpBaseResult.NotFound;
                        break;
                    case StatusCodes.Status500InternalServerError:
                        result = HttpBaseResult.InternalServerError;
                        break;
                }
                if (result != null)
                {
                    string content = JsonConvert.SerializeObject(result, jsonSerializerSettings);
                    await context.HttpContext.Response.WriteAsync(content, Encoding.UTF8);
                }
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
