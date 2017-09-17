using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BroccoliScraper
{
    class Food2ForkScraper : RecipeScraper
    {
        //yeah, yeah, don't put api keys in code, I know
        private const string Food2ForkApiKey = "b7faa25dc7600e8027ba79ed9c59d6b8";
        private const string searchUrl = "http://food2fork.com/api/search";
        private const string getUrl = "http://food2fork.com/api/get";

        private static readonly HttpClient client = new HttpClient();

        public override Recipe GetRecipe(string search)
        {
            var searchParams = new Dictionary<string, string>()
            {
                { "key", Food2ForkApiKey },
                { "q", search }
            };
            var searchResponse = client.PostAsync(searchUrl, new FormUrlEncodedContent(searchParams)).Result;
            JObject searchResponseObj = JObject.Parse(searchResponse.Content.ReadAsStringAsync().Result);
            var recipeUrl = (string)(searchResponseObj["recipes"][0]["f2f_url"]);
            string recipeId = recipeUrl.Split('/').Last();
            var getParams = new Dictionary<string, string>()
            {
                { "key", Food2ForkApiKey },
                { "rId", recipeId }
            };
            var getResponse = client.PostAsync(getUrl, new FormUrlEncodedContent(getParams)).Result;
            JObject getResponseObj = JObject.Parse(getResponse.Content.ReadAsStringAsync().Result);
            JArray ingredients = (JArray)(getResponseObj["recipe"]["ingredients"]);
            Recipe recipe = new Recipe();
            recipe.Name = (string)(getResponseObj["recipe"]["title"]);
            recipe.Ingredients = new List<Ingredient>();
            recipe.IngredientsNormalized = new List<Ingredient>();
            foreach (string ingredientStr in ingredients)
            {
                Ingredient next = new Ingredient(ingredientStr);
                recipe.Ingredients.Add(new Ingredient(ingredientStr));
                recipe.IngredientsNormalized.Add(Ingredient.NormalizeUnits(next));
            }
            return recipe;
        }
    }

    /* TTTTTTT H   H  OOO  M      M   AA   SSSSS     TTTTTTT H  H  EEEEE     DDDD    AA    N   N  K  K     EEEEE  N   N  GGG  IIIII N   N EEEEE
     *    T    H   H O   O MM    MM  A  A  S            T    H  H  E         D   D  A  A   NN  N  K K      E      NN  N G       I   NN  N E
     *    T    HHHHH O   O M M  M M AAAAAA SSSSS        T    HHHH  EEEEE     D   D AAAAAA  N N N  KK       EEEEE  N N N G  GG   I   N N N EEEEE
     *    T    H   H O   O M  MM  M A    A     S        T    H  H  E         D   D A    A  N  NN  K K      E      N  NN G   G   I   N  NN E
     *    T    H   H  OOO  M      M A    A SSSSS        T    H  H  EEEEE     DDDD  A    A  N   N  K  K     EEEEE  N   N  GGG  IIIII N   N EEEEE
     */
}
 