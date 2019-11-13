using HappyDog.Domain;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using AutoMapper;
using HappyDog.Domain.DataTransferObjects;
using Microsoft.Extensions.Hosting;
using Edi.Captcha;
using HappyDog.Infrastructure.Email;

namespace HappyDog.WebUI
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
            services.AddControllersWithViews();

            string conn = Configuration.GetConnectionString("HappyDog");
            services.AddDbContext<HappyDogContext>(option => option.UseSqlite(conn));

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddScoped<ArticleService>();
            services.AddScoped<UserService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CommentService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<NetEase126Sender>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            services.AddSessionBasedCaptcha(options =>
            {
                options.Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";
                options.SessionName = "CaptchaCode";
                options.CodeLength = 4;
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePages(async context =>
            {
                string html = context.HttpContext.Response.StatusCode.ToString();
                switch (context.HttpContext.Response.StatusCode)
                {
                    case StatusCodes.Status404NotFound:
                        html = await File.ReadAllTextAsync(Path.Combine(env.WebRootPath, "404.html"));
                        break;
                }
                await context.HttpContext.Response.WriteAsync(html);
            });

            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Article}/{action=Index}/{id?}");
            });
        }
    }
}
