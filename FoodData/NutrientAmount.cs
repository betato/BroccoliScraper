using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    class NutrientAmount
    {
        public int FoodId { get; private set; }
        public int NutrientID { get; private set; }
        public float NutrientValue { get; private set; }
        public float StandardError { get; private set; }
        public int NumberofObservations { get; private set; }
        public int NutrientSourceID { get; private set; }

        public NutrientAmount(string line)
        {
            try
            {
                string[] split = line.Split(',');
                FoodId = Int32.Parse(split[0]);
                NutrientID = Int32.Parse(split[1]);
                NutrientValue = float.Parse(split[2]);
                StandardError = float.Parse(split[3]);
                NumberofObservations = Int32.Parse(split[4]);
                NutrientSourceID = Int32.Parse(split[5]);
            }
            catch (Exception)
            {

            }
        }
    }
}
