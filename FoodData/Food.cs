using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    class Food
    {
        public string[] Keywords { get; private set; }
        public float Mass { get; private set; }

        public string NutrientValue{ get; private set; }

        public Food()
        {

        }
    }
}
