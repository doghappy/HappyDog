using System;
using System.Text;
using AutoMapper;
using HappyDog.Domain;
using HappyDog.Domain.DataTransferObjects;
using HappyDog.Domain.Entities;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Identity;
using HappyDog.Domain.IServices;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HappyDog.Console.Api
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

        readonly JsonSerializerSettings jsonSerializerSettings;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("Console", builder =>
            //    {
            //        string[] origins = Configuration.GetSection("Origins").Get<string[]>();
            //        builder.WithOrigins(origins);
            //        builder.AllowAnyMethod();
            //        builder.AllowAnyHeader();
            //        builder.AllowCredentials();
            //    });
            //});
            services.AddControllers();

            string conn = Configuration.GetConnectionString("HappyDog");
            services.AddDbContext<HappyDogContext>(option => option.UseSqlite(conn));

            services.AddIdentity<User, Role>().AddDefaultTokenProviders();
            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ITagService, TagService>();

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 9;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
                options.Events.OnRedirectToLogin = async context =>
                {
                    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    context.HttpContext.Response.StatusCode = 401;
                    var result = new HttpBaseResult
                    {
                        Message = "请登录。",
                        NoticeMode = NoticeMode.Info
                    };
                    string content = JsonConvert.SerializeObject(result, jsonSerializerSettings);
                    await context.HttpContext.Response.WriteAsync(content, Encoding.UTF8);
                };
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseExceptionHandler("/home/error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                HttpBaseResult result = null;
                switch (context.HttpContext.Response.StatusCode)
                {
                    case StatusCodes.Status404NotFound:
                        result = new HttpBaseResult
                        {
                            Message = "所请求的资源不在服务器上。",
                            NoticeMode = NoticeMode.Info
                        };
                        break;
                    case StatusCodes.Status415UnsupportedMediaType:
                        result = new HttpBaseResult
                        {
                            Message = "该请求是不受支持的类型。",
                            NoticeMode = NoticeMode.Warning
                        };
                        break;
                }
                if (result != null)
                {
                    string content = JsonConvert.SerializeObject(result, jsonSerializerSettings);
                    await context.HttpContext.Response.WriteAsync(content, Encoding.UTF8);
                }
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors("Console");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
