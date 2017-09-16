using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{

    class Ingredient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public float Quantity { get; set; }
    }

    class Recipe
    {

        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }

    }
}
