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
            AllRecipesScraper scraper = new AllRecipesScraper();
            Recipe recipe = scraper.GetRecipe("cake");
            Console.ReadKey(false);

            
        }
    }
}
