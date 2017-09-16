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
            "tbsp",
            "pound",
            "pounds",
            "pinch",
            "strip",
            "strips"
        };

        public Ingredient(string text)
        {
            text = text.Replace('(', ' ');
            text = text.Replace(')', ' ');
            string[] split = text.Split(' ');
            QuantityType firstType = ParseQuantity(split[0], out float firstQuantity);
            if (firstType == QuantityType.Fraction)
            {
                Quantity = firstQuantity;
                ParseUnitsFrom(split, 1);
            }
            else if (firstType == QuantityType.Number)
            {
                QuantityType secondType = ParseQuantity(split[1], out float secondQuantity);
                //we have a mixed fraction
                if (secondType == QuantityType.Fraction)
                {
                    Quantity = firstQuantity + secondQuantity;
                    ParseUnitsFrom(split, 2);
                }
                //it's just a number
                else
                {
                    Quantity = firstQuantity;
                    ParseUnitsFrom(split, 1);
                }
            }
            else
            {
                ParseUnitsFrom(split, 0);
            }
            if (Unit != null && Quantity == 0)
            {
                Quantity = 1;
            }
        }

        private void ParseUnitsFrom(string[] split, int idx)
        {
            if (IsUnit(split[idx]))
            {
                Unit = split[idx];
                Name = string.Join(" ", split.Skip(idx + 1).ToArray());
            }
            else
            {
                Name = string.Join(" ", split.Skip(idx).ToArray());
            }
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

        private bool IsUnit(string text)
        {
            return units.Contains(text, StringComparer.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return Quantity + " : " + Unit + " : " + Name;
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
            builder.Append(":\n");
            foreach (var ingredient in Ingredients)
            {
                builder.Append(ingredient.ToString());
                builder.Append("\n");
            }
            return builder.ToString();
        }

    }
}
