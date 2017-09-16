using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    abstract class RecipeScraper
    {
        public abstract Recipe GetRecipe(string search);
    }
}
