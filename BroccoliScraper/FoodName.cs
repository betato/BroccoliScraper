using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    class FoodName
    {
        public string[] Description { get; private set; }
        public int FoodId { get; private set; }
        public int Code { get; private set; }
        public int Group { get; private set; }
        public int Source { get; private set; }

        public FoodName(string line) {
            try
            {
                string[] split = line.Split(',');
                FoodId = Int32.Parse(split[0]);
                Code = Int32.Parse(split[1]);
                Group = Int32.Parse(split[2]);
                Source = Int32.Parse(split[3]);
                Description = split[5].Replace("[\" ]", "").Split(',');
            }
            catch (Exception)
            {

            }
        }
    }
}
