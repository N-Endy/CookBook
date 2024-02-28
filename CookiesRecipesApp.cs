namespace Cookbook;
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

    public void Run()
    {
        var allRecipes = _recipesRepository.Read(filepath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);

        _recipesUserInteraction.PromptToCreateRecipe();

        var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

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
            _recipesUserInteraction.ShowMessage(
                "No Ingredients has been specified. " +
                "Recipe will not be created.");
        }

        _recipesUserInteraction.Exit();
    }
}


interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
}


class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void Exit()
    {
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

interface IRecipesRepository
{

}


class RecipesRepository : IRecipesRepository
{
}