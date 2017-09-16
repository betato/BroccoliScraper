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

        public override string ToString()
        {
            return Quantity + " " + Unit + " " + Name;
        }
    }

    class Recipe
    {

        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Name);
            builder.Append(':');
            foreach (var ingredient in Ingredients)
            {
                builder.Append(ingredient.ToString());
                builder.Append("\n");
            }
            return builder.ToString();
        }

    }
}
