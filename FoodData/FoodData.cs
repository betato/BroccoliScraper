﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FoodCommon;

namespace FoodData
{
    class FoodData
    {
        public Dictionary<int, FoodName> foods = new Dictionary<int, FoodName>();
        public Dictionary<int, ConversionFactor> conversionFactors = new Dictionary<int, ConversionFactor>();
        public Dictionary<int, MeasureName> measureNames = new Dictionary<int, MeasureName>();

        public Dictionary<int, NutrientAmount> nutrientAmounts = new Dictionary<int, NutrientAmount>();
        public Dictionary<int, NutrientName> nutrientNames = new Dictionary<int, NutrientName>();

        public FoodData(string dir)
        {
            string[] lines = File.ReadAllLines(dir + "FOOD NAME.csv", Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                FoodName name = new FoodName(lines[i]);
                if (name != null && name.FoodId != 0)
                {
                    foods.Add(name.FoodId, name);
                }
            }

            lines = File.ReadAllLines(dir + "CONVERSION FACTOR.csv", Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                ConversionFactor cv = new ConversionFactor(lines[i]);
                //conversionFactors.Add(cv.FoodId, cv);
                if (!conversionFactors.ContainsKey(cv.FoodId))
                {
                    // Only add non-preexisting factors
                    conversionFactors.Add(cv.FoodId, cv);
                }
            }

            lines = File.ReadAllLines(dir + "MEASURE NAME.csv", Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                MeasureName mn = new MeasureName(lines[i]);
                if (!measureNames.ContainsKey(mn.MeasureId))
                {
                    measureNames.Add(mn.MeasureId, mn);
                }
            }

            lines = File.ReadAllLines(dir + "NUTRIENT AMOUNT.csv", Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                NutrientAmount na = new NutrientAmount(lines[i]);
                if (na != null && na.FoodId != 0)
                {
                    foods[na.FoodId].nutrients.Add(na);
                }
            }

            lines = File.ReadAllLines(dir + "NUTRIENT NAME.csv", Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++)
            {
                NutrientName nn = new NutrientName(lines[i]);
                if (!nutrientNames.ContainsKey(nn.NutrientID))
                {
                    nutrientNames.Add(nn.NutrientID, nn);
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
