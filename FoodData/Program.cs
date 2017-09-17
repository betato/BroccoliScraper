using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace BroccoliScraper
{
    class Program
    {

        static void Main(string[] args)
        {
            FoodData data = new FoodData(@"..\..\..\cnf-fcen-csv\");
            List<DbRow> rows = new List<DbRow>();
            foreach (var food in data.foods.Values)
            {
                if (food.Description != null)
                {
                    DbRow row = new DbRow();
                    row.Description = food.Description;
                    Measure measure = data.getMeasure(food);
                    if (measure == null)
                    {
                        continue;
                    }
                    if (measure.Unit == UnitType.None)
                    {
                        continue;
                    }
                    row.MeasureType = measure.Unit;
                    row.Nutrients = new List<Nutrient>();
                    foreach (var nutrientAmount in food.nutrients)
                    {
                        var newNutrient = new Nutrient();
                        var nutrientName = data.nutrientNames[nutrientAmount.NutrientID];
                        newNutrient.Name = nutrientName.Name;
                        newNutrient.Symbol = nutrientName.NutrientSymbol;
                        newNutrient.Unit = nutrientName.NutrientUnit;
                        newNutrient.Quantity = nutrientAmount.NutrientValue;
                        row.Nutrients.Add(newNutrient);
                    }
                    rows.Add(row);
                }
            }
            JsonConverter
        }
    }
}
