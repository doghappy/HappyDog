using HappyDog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace HappyDog.RecoverData
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false);
            var configuration = builder.Build();
            string connDev = configuration["ConnectionStrings:HappyDog:Development"];
            string connProd = configuration["ConnectionStrings:HappyDog:Production"];
            var optionDev = new DbContextOptionsBuilder<HappyDogContext>()
                .UseSqlite(connDev)
                .Options;
            var optionProd = new DbContextOptionsBuilder<HappyDogContext>()
                .UseSqlite(connProd)
                .Options;

            using (var dbDev = new HappyDogContext(optionDev))
            using (var dbProd = new HappyDogContext(optionProd))
            {
                var articles = dbProd.Articles.ToList();
                dbDev.Articles.AddRange(articles);

                var categories = dbProd.Categories.ToList();
                dbDev.Categories.AddRange(categories);

                dbDev.SaveChanges();
            }

            Console.WriteLine("Recover data success");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
