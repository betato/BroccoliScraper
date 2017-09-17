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
            foreach (var food in data.foods)
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
                }
            }
        }
    }
}
