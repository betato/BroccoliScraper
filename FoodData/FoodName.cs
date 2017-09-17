using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace FoodData
{
    class FoodName
    {
        public string[] Description { get; private set; }
        public int FoodId { get; private set; }
        public int Code { get; private set; }
        public int Group { get; private set; }
        public int Source { get; private set; }
        public List<NutrientAmount> nutrients = new List<NutrientAmount>();

        Regex betweenQuotes = new Regex("\".*?\"");

        public FoodName(string line) {
            try
            {
                string[] split = line.Split(',');
                FoodId = Int32.Parse(split[0]);
                Code = Int32.Parse(split[1]);
                Group = Int32.Parse(split[2]);
                Source = Int32.Parse(split[3]);

                if (line.Contains('"'))
                {
                    // Split quote delimited sections
                    Description = betweenQuotes.Matches(line)[0].ToString().Split(',');
                    for (int i = 0; i < Description.Length; i++)
                    {
                        Description[i] = Description[i].Replace("\"", string.Empty);
                        Description[i] = Description[i].Trim();
                    }
                }
                else
                {
                    Description = new string[] { split[4] };
                }
                
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing food name");
            }
        }
    }
}
