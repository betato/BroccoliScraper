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
            HtmlWeb webGet = new HtmlWeb();
            //var doc = webGet.Load("http://allrecipes.com/search/results/?wt=croissant&sort=re");
            var doc = webGet.Load("http://www.food.com/search/croissant");
            var root = doc.DocumentNode;

            
        }
    }
}
