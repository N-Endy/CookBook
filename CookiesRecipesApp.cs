namespace Cookbook;
class CookiesRecipesApp
{
    private readonly RecipesRepository _recipesRepository;
    private readonly RecipeUserInteraction _recipeUserInteraction;

    public CookiesRecipesApp(
        RecipesRepository recipesRepository,
        RecipeUserInteraction recipeUserInteraction
        )
    {
        _recipesRepository = recipesRepository;
        _recipeUserInteraction = recipeUserInteraction;
    }

    public void Run()
    {
        var allRecipes = _recipesRepository.Read(filepath);
        _recipeUserInteraction.PrintExistingRecipes(allRecipes);

        _recipeUserInteraction.PromptToCreateRecipe();

        var ingredients = _recipeUserInteraction.ReadIngredientsFromUser();

        if(ingredients.Count > 0)
        {
            var recipes = new Recipe(ingredients);
            allRecipes.Add(recipes);
            _recipesRepository.Write(allRecipes, filepath);
            _recipesRepository.ShowMessage("Recipe Added:");
            _recipesRepository.ShowMessage(recipes.ToString());
        }
        else
        {
            _recipeUserInteraction.ShowMessage(
                "No Ingredients has been specified. " +
                "Recipe will not be created.");
        }

        _recipesRepository.Exit();
    }
}



