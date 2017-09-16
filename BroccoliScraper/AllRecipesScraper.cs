using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BroccoliScraper
{
    class AllRecipesScraper : IRecipeScraper
    {

        private const string searchUrl = "http://allrecipes.com/search/results/?wt=???&sort=re";
        private const string recipeBaseUrl = "http://allrecipes.com";

        private string[] units =
        {
            "tablespoon",
            "tablespoons",
            "teaspoon",
            "teaspoons",
            "cup",
            "cups",
            "ounce",
            "ounces"
        };

        public Recipe GetRecipe(string search)
        {
            string recipeUrl  = recipeBaseUrl + FindRecipePage(search);
            var web = new HtmlWeb();
            var doc = web.Load(recipeUrl);
            string title = doc.DocumentNode.SelectNodes("//*[contains(@class, 'recipe-summary__h1')]").First().InnerText;
            List<HtmlNode> checklistEntries = doc.DocumentNode.SelectNodes("//*[contains(@class, 'checkList__line')]").ToList();
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var node in checklistEntries)
            {
                if (isIngredientEntry(node))
                {
                    ingredients.Add(parseIngredient(node));
                }
            }
            Recipe recipe = new Recipe();
            recipe.Name = title;
            recipe.Ingredients = ingredients;
            //there's a mysterious blank ingredient at the end, so we remove it
            recipe.Ingredients.RemoveAt(recipe.Ingredients.Count - 1);
            return recipe;
        }

        private string FindRecipePage(string search)
        {
            var queryUrl = searchUrl.Replace("???", search);
            var web = new HtmlWeb();
            var doc = web.Load(queryUrl);
            var resultsContainer = doc.GetElementbyId("searchResultsApp").ChildNodes.Where(node => node.Id == "grid").First();
            HtmlNode firstResult = null;
            foreach (var result in resultsContainer.ChildNodes)
            {
                if (isRecipe(result))
                {
                    firstResult = result;
                    break;
                }
            }
            return firstResult.ChildNodes[5].Attributes["href"].DeEntitizeValue;
        }

        private bool isRecipe(HtmlNode node)
        {
            if (node.Name == "article" && node.Id != "dfp_container")
            {
                if (node.Attributes["class"].DeEntitizeValue.Contains("grid-col--fixed-tiles"))
                {
                    if (!node.Attributes["class"].DeEntitizeValue.Contains("hub-card"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool isIngredientEntry(HtmlNode entry)
        {
            return !(entry.ChildNodes[1].Id == "btn-addtolist");
        }

        private Ingredient parseIngredient(HtmlNode ingredientNode)
        {
            string text = ingredientNode.ChildNodes[1].ChildNodes[3].InnerText;
            text = text.Replace('(', ' ');
            text = text.Replace(')', ' ');
            Ingredient ingredient = new Ingredient();
            string[] split = text.Split(' ');
            if (ParseQuantity(split[0], out float quantity))
            {
                ingredient.Quantity = quantity;
                if (units.Contains(split[1]))
                {
                    ingredient.Unit = split[1];
                    ingredient.Name = string.Join(" ", split.Skip(2).ToArray());
                }
                else
                {
                    ingredient.Name = string.Join(" ", split.Skip(1).ToArray());
                }
            }
            return ingredient;
        }

        private bool ParseQuantity(string text, out float quantity)
        {
            if (float.TryParse(text, out float result))
            {
                quantity = result;
                return true;
            }
            else if (text.Contains('/'))
            {
                var fractionSplit = text.Split('/');
                if (float.TryParse(fractionSplit[0], out float numerator) && float.TryParse(fractionSplit[1], out float denominator))
                {
                    quantity = numerator / denominator;
                    return true;
                }
            }
            quantity = float.NaN;
            return false;
        }
    }

}
