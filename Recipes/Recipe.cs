using System.Collections;
using System.Collections.Generic;

namespace Cookbook.Recipes;
public class Recipe
{
    public IEnumerable<Ingredient> Ingredients { get; }

    public Recipe(IEnumerable<Ingredient> ingredients)
    {
        Ingredients = ingredients;
    }
}