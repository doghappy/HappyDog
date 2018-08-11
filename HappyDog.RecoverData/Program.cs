using HappyDog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HappyDog.RecoverData
{
    class Program
    {
        static void Main(string[] args)
        {
            //var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false);
            //var configuration = builder.Build();
            //string connDev = configuration["ConnectionStrings:HappyDog:Development"];
            //string connProd = configuration["ConnectionStrings:HappyDog:Production"];
            //var optionDev = new DbContextOptionsBuilder<HappyDogContext>()
            //    .UseSqlite(connDev)
            //    .Options;
            //var optionProd = new DbContextOptionsBuilder<HappyDogContext>()
            //    .UseSqlite(connProd)
            //    .Options;

            //using (var dbDev = new HappyDogContext(optionDev))
            //using (var dbProd = new HappyDogContext(optionProd))
            //{
            //    var articles = dbProd.Articles.ToList();
            //    dbDev.Articles.AddRange(articles);

            //    var categories = dbProd.Categories.ToList();
            //    dbDev.Categories.AddRange(categories);

            //    dbDev.SaveChanges();
            //}

            //Console.WriteLine("Recover data success");
            //Console.WriteLine("Press any key to exit");we

            //string[] cultureNames = { "en-US", "se-SE" };
            //string[] strings1 = { "case", "encyclopædia", "encyclopædia", "Archæology" };
            //string[] strings2 = { "Case", "encyclopaedia", "encyclopedia", "ARCHÆOLOGY" };
            //StringComparison[] comparisons = (StringComparison[])Enum.GetValues(typeof(StringComparison));

            //foreach (var cultureName in cultureNames)
            //{
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            //    Console.WriteLine("Current Culture: {0}", CultureInfo.CurrentCulture.Name);
            //    for (int ctr = 0; ctr <= strings1.GetUpperBound(0); ctr++)
            //    {
            //        foreach (var comparison in comparisons)
            //            Console.WriteLine("   {0} = {1} ({2}): {3}", strings1[ctr],
            //                              strings2[ctr], comparison,
            //                              String.Equals(strings1[ctr], strings2[ctr], comparison));

            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"));


            Console.ReadKey();
        }
    }
}
