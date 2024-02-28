namespace Cookbook;
public class CookiesRecipesApp
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

        _recipeUserInteraction.Exit();
    }
}


class RecipeUserInteraction
{
    internal void PrintExistingRecipes(object allRecipes)
    {
        throw new NotImplementedException();
    }

    internal void PromptToCreateRecipe()
    {
        throw new NotImplementedException();
    }

    internal object ReadIngredientsFromUser()
    {
        throw new NotImplementedException();
    }

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


class RecipesRepository
{
    internal object Read(object filepath)
    {
        throw new NotImplementedException();
    }

    internal void ShowMessage(string v)
    {
        throw new NotImplementedException();
    }

    internal void Write(object allRecipes, object filepath)
    {
        throw new NotImplementedException();
    }
}