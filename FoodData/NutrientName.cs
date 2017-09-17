using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoodData
{
    class NutrientName
    {
        public int NutrientID { get; private set; }
        public int NutrientCode { get; private set; }
        public string NutrientSymbol { get; private set; }
        public string NutrientUnit { get; private set; }
        public string[] Name { get; private set; }

        Regex betweenQuotes = new Regex("\".*?\"");

        public NutrientName(string line)
        {
            try
            {
                string[] split = line.Split(',');
                NutrientID = Int32.Parse(split[0]);
                NutrientCode = Int32.Parse(split[1]);
                NutrientSymbol = split[2];
                NutrientUnit = split[3];

                if (line.Contains('"'))
                {
                    // Split quote delimited sections
                    Name = betweenQuotes.Matches(line)[0].ToString().Split(',');
                    for (int i = 0; i < Name.Length; i++)
                    {
                        Name[i] = Name[i].Replace("\"", string.Empty);
                        Name[i] = Name[i].Trim();
                    }
                }
                else
                {
                    Name = new string[] { split[4] };
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing nutrient name");
            }
        }
    }
}
