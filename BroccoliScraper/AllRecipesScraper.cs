﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BroccoliScraper
{
    class AllRecipesScraper : RecipeScraper
    {

        private const string searchUrl = "http://allrecipes.com/search/results/?wt=???&sort=re";
        private const string recipeBaseUrl = "http://allrecipes.com";

        public override Recipe GetRecipe(string search)
        {
            string recipeUrl  = recipeBaseUrl + FindRecipePage(search);
            var web = new HtmlWeb();
            var doc = web.Load(recipeUrl);
            string title = doc.DocumentNode.SelectNodes("//*[contains(@class, 'recipe-summary__h1')]").First().InnerText;
            List<HtmlNode> checklistEntries = doc.DocumentNode.SelectNodes("//*[contains(@class, 'checkList__line')]").ToList();
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Ingredient> normalizedIngredients = new List<Ingredient>();
            foreach (var node in checklistEntries)
            {
                if (isIngredientEntry(node))
                {
                    Ingredient next = new Ingredient(node.ChildNodes[1].ChildNodes[3].InnerText);
                    ingredients.Add(next);
                    normalizedIngredients.Add(Ingredient.NormalizeUnits(next));
                }
            }
            Recipe recipe = new Recipe();
            recipe.Name = title;
            recipe.Ingredients = ingredients;
            recipe.IngredientsNormalized = normalizedIngredients;
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
            foreach (HtmlNode node in firstResult.ChildNodes)
            {
                if (node.Name == "a")
                {
                    return node.Attributes["href"].DeEntitizeValue;
                }
            }
            return null;
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

    }

}
