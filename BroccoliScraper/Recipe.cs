using FoodCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{

    class Ingredient
    {

        public string Name { get; set; } = null;
        public UnitType Unit { get; set; } = UnitType.None;
        public float Quantity { get; set; } = 0.0f;

        private Dictionary<string, UnitType> units = new Dictionary<string, UnitType>
        {
            { "tablespoon", UnitType.Tablespoon },
            { "tablespoons", UnitType.Tablespoon },
            { "teaspoon", UnitType.Teaspoon },
            { "teaspoons", UnitType.Teaspoon },
            { "cup", UnitType.Cup },
            { "cups", UnitType.Cup },
            { "ounce", UnitType.Ounce },
            { "ounces", UnitType.Ounce },
            { "tbsp", UnitType.Tablespoon },
            { "pound", UnitType.Pound },
            { "pounds", UnitType.Pound },
            { "pinch", UnitType.Pinch },
        };

        public Ingredient()
        {

        }

        public Ingredient(string text)
        {
            text = text.Replace('(', ' ');
            text = text.Replace(')', ' ');
            string[] split = text.Split(' ');
            Util.QuantityType firstType = Util.ParseQuantity(split[0], out float firstQuantity);
            if (firstType == Util.QuantityType.Fraction)
            {
                Quantity = firstQuantity;
                ParseUnitsFrom(split, 1);
            }
            else if (firstType == Util.QuantityType.Number)
            {
                Util.QuantityType secondType = Util.ParseQuantity(split[1], out float secondQuantity);
                //we have a mixed fraction
                if (secondType == Util.QuantityType.Fraction)
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

        public static Ingredient NormalizeUnits(Ingredient ingredient)
        {
            Ingredient newIngredient = new Ingredient();
            newIngredient.Name = ingredient.Name;
            switch (ingredient.Unit)
            {
                case UnitType.Cup:
                    newIngredient.Quantity = ingredient.Quantity * 236.588f;
                    newIngredient.Unit = UnitType.Millilitre;
                    break;
                case UnitType.Ounce:
                    newIngredient.Quantity = ingredient.Quantity * 28.3495f;
                    newIngredient.Unit = UnitType.Gram;
                    break;
                case UnitType.Pinch:
                    newIngredient.Quantity = ingredient.Quantity * 4.92892f;
                    newIngredient.Unit = UnitType.Millilitre;
                    break;
                case UnitType.Pound:
                    newIngredient.Quantity = ingredient.Quantity * 453.592f;
                    newIngredient.Unit = UnitType.Gram;
                    break;
                case UnitType.Tablespoon:
                    newIngredient.Quantity = ingredient.Quantity * 14.7868f;
                    newIngredient.Unit = UnitType.Millilitre;
                    break;
                case UnitType.Teaspoon:
                    newIngredient.Quantity = ingredient.Quantity * 4.92892f;
                    newIngredient.Unit = UnitType.Millilitre;
                    break;
            }
            return newIngredient;
            
        }

        private void ParseUnitsFrom(string[] split, int idx)
        {
            if (IsUnit(split[idx]))
            {
                Unit = units[split[idx]];
                Name = string.Join(" ", split.Skip(idx + 1).ToArray());
            }
            else
            {
                Name = string.Join(" ", split.Skip(idx).ToArray());
            }
        }

        private bool IsUnit(string text)
        {
            return units.Keys.Contains(text, StringComparer.OrdinalIgnoreCase);
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
        public List<Ingredient> IngredientsNormalized { get; set; }

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
