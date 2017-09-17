using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    class ConversionFactor
    {
        public int FoodId { get; private set; }
        public int MeasureId { get; private set; }
        public float Factor { get; private set; }

        public ConversionFactor(string line)
        {
            try
            {
                string[] split = line.Split(',');
                FoodId = Int32.Parse(split[0]);
                MeasureId = Int32.Parse(split[1]);
                Factor = float.Parse(split[2]);
            }
            catch (Exception)
            {
                
            }
        }
    }
}
