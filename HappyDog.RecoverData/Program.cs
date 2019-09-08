using HappyDog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HappyDog.RecoverData
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            string connDev = configuration["ConnectionStrings:HappyDog:Development"];
            var optionDev = new DbContextOptionsBuilder<HappyDogContext>()
                .UseSqlServer(connDev)
                .Options;

            using (var dbDev = new HappyDogContext(optionDev))
            {
                var articles = dbDev.Articles.ToList();
                foreach (var item in articles)
                {
                    dbDev.ArticleContents.Add(new Domain.Entities.ArticleContent
                    {
                        Id = item.Id,
                        Content = item.Content
                    });
                }
            }

            Console.WriteLine("Recover data success");
        }
    }
}
