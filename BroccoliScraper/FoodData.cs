using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BroccoliScraper
{
    class FoodData
    {
        public List<FoodName> foods = new List<FoodName>();
        public Dictionary<int, ConversionFactor> conversionFactors = new Dictionary<int, ConversionFactor>();
        //public List<ConversionFactor> conversionFactors = new List<ConversionFactor>();
        public Dictionary<int, MeasureName> measureNames = new Dictionary<int, MeasureName>();

        public FoodData(string dir)
        {
            string[] lines = File.ReadAllLines(dir + "FOOD NAME.csv");
            foreach (string line in lines)
            {
                foods.Add(new FoodName(line));
            }

            lines = File.ReadAllLines(dir + "CONVERSION FACTOR.csv");
            foreach (string line in lines)
            {
                ConversionFactor cv = new ConversionFactor(line);
                //conversionFactors.Add(cv.FoodId, cv);
                if (!conversionFactors.ContainsKey(cv.FoodId))
                {
                    // Only add non-preexisting factors
                    conversionFactors.Add(cv.FoodId, cv);
                }
            }

            lines = File.ReadAllLines(dir + "MEASURE NAME.csv");
            foreach (string line in lines)
            {
                MeasureName mn = new MeasureName(line);
                if (!conversionFactors.ContainsKey(mn.MeasureId))
                {
                    measureNames.Add(mn.MeasureId, mn);
                }
            }
        }

        public void getMeasure(FoodName food)
        {
            ConversionFactor cv = conversionFactors[food.FoodId];
            MeasureName mn = measureNames[cv.MeasureId];
            
            Console.WriteLine();
        }
    }
}
