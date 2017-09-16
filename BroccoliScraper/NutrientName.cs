using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    class NutrientName
    {
        public int NutrientID { get; private set; }
        public int NutrientCode { get; private set; }
        public string NutrientSymbol { get; private set; }
        public string NutrientUnit { get; private set; }
        public string Name { get; private set; }

        public NutrientName(string line)
        {
            try
            {
                string[] split = line.Split(',');
                NutrientID = Int32.Parse(split[0]);
                NutrientCode = Int32.Parse(split[1]);
                NutrientSymbol = split[2];
                NutrientUnit = split[3];
                Name = split[4];
            }
            catch (Exception)
            {

            }
        }
    }
}
