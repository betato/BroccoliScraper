using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
using System.Web;
using System.IO;

namespace BroccoliScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            Food2ForkScraper scraper = new Food2ForkScraper();
            Recipe recipe = scraper.GetRecipe("scrambled eggs");
            Console.WriteLine(recipe.ToString());
            FoodData data = new FoodData(@"..\..\..\cnf-fcen-csv\");
            using (StreamWriter writer = new StreamWriter(new FileStream("out.txt", FileMode.Create, FileAccess.Write)))
            {
                foreach (var food in data.foods)
                {
                    if (food.Description != null)
                    {
                        writer.Write(string.Join(", ", food.Description));
                        //Console.WriteLine(food.FoodId);
                        writer.Write(data.getMeasure(food));
                        writer.Write("\n");
                    }
                }
            }
            Console.ReadKey(true);
        }
    }
}
