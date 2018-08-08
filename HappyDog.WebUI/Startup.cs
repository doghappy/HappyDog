using HappyDog.Domain;
using AutoMapper;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HappyDog.Domain.DataTransferObjects;
using HappyDog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using HappyDog.Domain.Identity;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.SlidingExpiration = true;
            //        options.LoginPath = "/User/SignIn";
            //        options.LogoutPath = "/User/SignOut";
            //        options.AccessDeniedPath = "/AccessDenied.html";
            //    });

            string conn = Configuration.GetConnectionString("HappyDog");
            services.AddDbContext<HappyDogContext>(option => option.UseSqlite(conn));

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

            services.AddScoped<ArticleService>();
            services.AddScoped<UserService>();
            services.AddScoped<CategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseAuthentication();
            //app.UseCookiePolicy();

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.HttpContext.Request.Path = "/Home/NotFound";
                    await context.Next(context.HttpContext);
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Article}/{action=Index}/{id?}");
            });
        }
    }
}
