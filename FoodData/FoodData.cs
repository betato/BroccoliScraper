using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FoodCommon;

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
                if (!measureNames.ContainsKey(mn.MeasureId))
                {
                    measureNames.Add(mn.MeasureId, mn);
                }
            }
        }

        public Measure getMeasure(FoodName food)
        {
            if (!conversionFactors.ContainsKey(food.FoodId))
            {
                Console.WriteLine("No factor for {0}", string.Join(":", food.Description));
                return null;
            }
            ConversionFactor cv = conversionFactors[food.FoodId];
            if (!measureNames.ContainsKey(cv.MeasureId))
            {
                Console.WriteLine("No measure for {0}", string.Join(":", food.Description));
                return null;
            }
            MeasureName mn = measureNames[cv.MeasureId];
            Measure measure = new Measure();
            string[] split = mn.Description.Split(' ');
            for (int i = split[0].Length; i > 0; i--)
            {
                if (Util.ParseQuantity(split[0].Substring(0, i), out float quantity) != Util.QuantityType.None)
                {
                    measure.Quantity = quantity;
                    string unitStr = split[0].Substring(i);
                    if (unitStr == "g")
                    {
                        measure.Unit = UnitType.Gram;
                    }
                    else if (unitStr == "ml")
                    {
                        measure.Unit = UnitType.Millilitre;
                    }
                    else
                    {
                        measure.Unit = UnitType.None;
                    }
                    return measure;
                }
            }
           
            return null;
        }

        
    }
}
