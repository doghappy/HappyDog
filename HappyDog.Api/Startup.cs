using NSwag;
using System.Text;
using AutoMapper;
using HappyDog.Domain;
using HappyDog.Domain.Enums;
using HappyDog.Domain.Models.Results;
using HappyDog.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using HappyDog.Domain.DataTransferObjects;
using Microsoft.Extensions.Hosting;
using HappyDog.Domain.IServices;

namespace HappyDog.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Console", builder =>
                {
                    string[] origins = Configuration.GetSection("Origins").Get<string[]>();
                    builder.WithOrigins(origins);
                });
            });
            services.AddControllers();

            string conn = Configuration.GetConnectionString("HappyDog");
            services.AddDbContext<HappyDogContext>(option => option.UseSqlite(conn));

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            #region IoC Service
            // Transient：瞬时（Transient）生命周期服务在它们每次请求时被创建。这一生命周期适合轻量级的，无状态的服务。

            // Scoped：作用域（Scoped）生命周期服务在每次请求被创建一次。

            // Singleton：单例（Singleton）生命周期服务在它们第一次被请求时创建。
            // 或者如果你在 ConfigureServices 运行时指定一个实例
            // 并且每个后续请求将使用相同的实例。如果你的应用程序需要单例行为，
            // 建议让服务容器管理服务的生命周期而不是在自己的类中实现单例模式和管理对象的生命周期。

            //https://stackoverflow.com/questions/38138100/what-is-the-difference-between-services-addtransient-service-addscope-and-servi

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            services.AddSwaggerDocument(configure =>
            {
                configure.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "doghappy.wang API";
                    document.Info.Description = "A simple ASP.NET Core Web Api, UI by NSwag.";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "HeroWong",
                        Email = "hero_wong@outlook.com",
                        Url = "https://doghappy.wang"
                    };
                };
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
            }

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
                    string content = JsonConvert.SerializeObject(result, _jsonSerializerSettings);
                    await context.HttpContext.Response.WriteAsync(content, Encoding.UTF8);
                }
            });

            app.UseHttpsRedirection();
            app.UseOpenApi(configure =>
            {
                configure.PostProcess = (document, req) =>
                {
                    document.Schemes = new[] { OpenApiSchema.Https };
                };
            });
            app.UseSwaggerUi3();
            app.UseReDoc();
            app.UseRouting();
            app.UseCors("Console");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
