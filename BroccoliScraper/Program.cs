using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
using System.Web;

namespace BroccoliScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            Food2ForkScraper scraper = new Food2ForkScraper();
            Recipe recipe = scraper.GetRecipe("scrambled eggs");
            Console.WriteLine(recipe.ToString());
            Console.ReadKey(true);
            new FoodData(@"..\..\..\cnf-fcen-csv\");
        }
    }
}
