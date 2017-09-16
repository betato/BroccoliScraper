using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    abstract class RecipeScraper
    {

        private string[] units =
        {
            "tablespoon",
            "tablespoons",
            "teaspoon",
            "teaspoons",
            "cup",
            "cups",
            "ounce",
            "ounces"
        };

        public abstract Recipe GetRecipe(string search);

        protected Ingredient parseIngredient(string text)
        {
            text = text.Replace('(', ' ');
            text = text.Replace(')', ' ');
            Ingredient ingredient = new Ingredient();
            string[] split = text.Split(' ');
            if (ParseQuantity(split[0], out float quantity))
            {
                ingredient.Quantity = quantity;
                if (units.Contains(split[1]))
                {
                    ingredient.Unit = split[1];
                    ingredient.Name = string.Join(" ", split.Skip(2).ToArray());
                }
                else
                {
                    ingredient.Name = string.Join(" ", split.Skip(1).ToArray());
                }
            }
            else
            {
                ingredient.Name = text;
            }
            return ingredient;
        }

        private bool ParseQuantity(string text, out float quantity)
        {
            if (float.TryParse(text, out float result))
            {
                quantity = result;
                return true;
            }
            else if (text.Contains('/'))
            {
                var fractionSplit = text.Split('/');
                if (float.TryParse(fractionSplit[0], out float numerator) && float.TryParse(fractionSplit[1], out float denominator))
                {
                    quantity = numerator / denominator;
                    return true;
                }
            }
            quantity = float.NaN;
            return false;
        }

    }
}
