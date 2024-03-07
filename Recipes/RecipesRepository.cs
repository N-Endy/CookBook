using Cookbook.Recipes;
using Cookbook.Recipes.Ingredients;

public class RecipesRepository : IRecipesRepository
{
    private readonly IStringsRepository _stringsRepository;
    private readonly IngredientsRegister _ingredientsRegister;
    private const string Separator = ",";

    public RecipesRepository(IStringsRepository stringsRepository, IngredientsRegister ingredientsRegister)
    {
        _stringsRepository = stringsRepository;
        _ingredientsRegister = ingredientsRegister;
    }

    public List<Recipe> Read(string filepath)
    {
        var recipesFromFile = _stringsRepository.Read(filepath);
        var recipes = new List<Recipe>();

        foreach (var recipeId in recipesFromFile)
        {
            recipes.Add(GetRecipeById(recipeId));
        }

        return recipes;
    }

    private Recipe GetRecipeById(string recipeFromFile)
    {
        var textualIds = recipeFromFile.Split(Separator);
        var ingredients = new List<Ingredient>();

        foreach (var textualId in textualIds)
        {
            var id = int.Parse(textualId);
            var ingredient = _ingredientsRegister.GetById(id);
            ingredients.Add(ingredient);
        }

        return new Recipe(ingredients);
    }

    public void Write(List<Recipe> allRecipes, string filepath)
    {
        var recipesAssString = new List<string>();
        foreach (var recipe in allRecipes)
        {
            var allIds = new List<int>();
            foreach (var ingredient in recipe.Ingredients)
            {
                allIds.Add(ingredient.Id);
            }
            recipesAssString.Add(string.Join(Separator, allIds));
        }
        _stringsRepository.Write(filepath, recipesAssString);
    }
}
