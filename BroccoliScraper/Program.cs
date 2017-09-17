using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace BroccoliScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            Food2ForkScraper scraper = new Food2ForkScraper();
            Recipe recipe = null;
            if (args.Length == 0)
            {
                recipe = scraper.GetRecipe("spring rolls");
            }
            else
            {
                recipe = scraper.GetRecipe(args[0]);
            }
            Console.WriteLine(JsonConvert.SerializeObject(recipe));
        }
    }
}
