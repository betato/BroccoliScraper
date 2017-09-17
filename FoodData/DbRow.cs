using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{

    class Measure
    {
        public float Quantity { get; set; }
        public UnitType Unit { get; set; }
    }

    class Nutrient
    {
        public string[] Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public string Symbol { get; set; }
    }

    class DbRow
    {
        public string[] Description { get; set; }
        public UnitType MeasureType { get; set; }
        public List<Nutrient> Nutrients { get; set; }
    }
}
