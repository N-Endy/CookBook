using Cookbook.Recipes;
using Cookbook.Recipes.Ingredients;

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction(new IngredientsRegister()));

cookiesRecipesApp.Run("recipes.txt");

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

        // var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        // if (ingredients.Count > 0)
        // {
        //     var recipes = new Recipe(ingredients);
        //     allRecipes.Add(recipes);
        //     _recipesRepository.Write(allRecipes, filepath);
        //     _recipesRepository.ShowMessage("Recipe Added:");
        //     _recipesRepository.ShowMessage(recipes.ToString());
        // }
        // else
        // {
        //     _recipesUserInteraction.ShowMessage(
        //         "No Ingredients has been specified. " +
        //         "Recipe will not be created.");
        // }

        _recipesUserInteraction.Exit();
    }
}


public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
}

public class IngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new CoconutFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamon(),
        new Eggs(),
        new Cinnamon(),
        new CocoaPowder(),
    };
}


public class RecipesConsoleUserInteraction : IRecipesUserInteraction
{
    private readonly IngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
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

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if (allRecipes.Any())
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);

            var counter = 1;
            foreach(var recipe in allRecipes)
            {
                Console.WriteLine($"*****{counter}*****`");
                Console.WriteLine(recipe);
                Console.WriteLine();
                ++counter;
            }
        }
    }

    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe " +
            "Available ingredients are:");
        foreach(var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filepath);
}


public class RecipesRepository : IRecipesRepository
{
    public List<Recipe> Read(string filepath)
    {
        return new List<Recipe>
            {
                new Recipe(new List<Ingredient>
                {
                    new Sugar(),
                    new Chocolate(),
                    new Butter(),
                }),
                new Recipe(new List<Ingredient>
                {
                    new Eggs(),
                    new Cinnamon()
                })
            };
    }
}