
using Cookbook.Recipes;

public class CookiesRecipesApp
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(
        IRecipesRepository recipesRepository,
        IRecipesUserInteraction recipeUserInteraction
        )
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipeUserInteraction;
    }

    public void Run(string filepath)
    {
        var allRecipes = _recipesRepository.Read(filepath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);

        _recipesUserInteraction.PromptToCreateRecipe();

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        if (ingredients.Any())
        {
            var recipes = new Recipe(ingredients);
            allRecipes.Add(recipes);
            _recipesRepository.Write(allRecipes, filepath);

            _recipesUserInteraction.ShowMessage("Recipe Added:");
            _recipesUserInteraction.ShowMessage(recipes.ToString());
        }
        else
        {
            _recipesUserInteraction.ShowMessage(
                "No Ingredients has been specified. " +
                "Recipe will not be created.");
        }

        _recipesUserInteraction.Exit();
    }
}
