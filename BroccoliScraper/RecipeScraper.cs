using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    abstract class RecipeScraper
    {

        enum QuantityType
        {
            Fraction,
            Number,
            None
        }

        private string[] units =
        {
            "tablespoon",
            "tablespoons",
            "teaspoon",
            "teaspoons",
            "cup",
            "cups",
            "ounce",
            "ounces",
            "tbsp"
        };

        public abstract Recipe GetRecipe(string search);

        protected Ingredient parseIngredient(string text)
        {
            text = text.Replace('(', ' ');
            text = text.Replace(')', ' ');
            Ingredient ingredient = new Ingredient();
            string[] split = text.Split(' ');
            QuantityType firstType = ParseQuantity(split[0], out float firstQuantity);
            if (firstType == QuantityType.Fraction)
            {
                ingredient.Quantity = firstQuantity;
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
            else if (firstType == QuantityType.Number)
            {
                QuantityType secondType = ParseQuantity(split[1], out float secondQuantity);
                //we have a mixed fraction
                if (secondType == QuantityType.Fraction)
                {
                    ingredient.Quantity = firstQuantity + secondQuantity;
                    if (units.Contains(split[2]))
                    {
                        ingredient.Unit = split[2];
                        ingredient.Name = string.Join(" ", split.Skip(3).ToArray());
                    }
                    else
                    {
                        ingredient.Name = string.Join(" ", split.Skip(2).ToArray());
                    }
                }
                //it's just a number
                else
                {
                    ingredient.Quantity = firstQuantity;
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
            }
            else
            {
                ingredient.Name = text;
            }
            return ingredient;
        }

        private QuantityType ParseQuantity(string text, out float quantity)
        {
            if (text == "A" || text == "a")
            {
                quantity = 1.0f;
                return QuantityType.Number;
            }
            if (float.TryParse(text, out float result))
            {
                quantity = result;
                return QuantityType.Number;
            }
            else if (text.Contains('/'))
            {
                var fractionSplit = text.Split('/');
                if (float.TryParse(fractionSplit[0], out float numerator) && float.TryParse(fractionSplit[1], out float denominator))
                {
                    quantity = numerator / denominator;
                    return QuantityType.Fraction;
                }
            }
            quantity = float.NaN;
            return QuantityType.None;
        }

    }
}
