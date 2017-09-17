using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodData
{
    class MeasureName
    {
        public int MeasureId { get; private set; }
        public string Description { get; private set; }

        public MeasureName(string line)
        {
            try
            {
                string[] split = line.Split(',');
                MeasureId = Int32.Parse(split[0]);
                Description = split[1].Replace("[\" ]", "");
            }
            catch (Exception)
            {
                Console.WriteLine("Error parsing measurement name");
            }
        }
    }
}
