using Cookbook.Recipes;
using Cookbook.Recipes.Ingredients;
using System.Text.Json;

const FileFormat Format = FileFormat.Json;

IStringsRepository stringsRepository = Format == FileFormat.Json ?
    new StringsJsonRepository() :
    new StringsTextualRepository();

const string FileName = "recipes";
var fileMetaData = new FileMetaData(FileName, Format);

var ingredientsRegister = new IngredientsRegister();

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(stringsRepository, ingredientsRegister),
    new RecipesConsoleUserInteraction(ingredientsRegister));

cookiesRecipesApp.Run(fileMetaData.ToPath());



public class FileMetaData
{
    public string FileName { get;}
    public FileFormat Format { get;}

    public FileMetaData(string fileName, FileFormat format)
    {
        FileName = fileName;
        Format = format;
    }

    public string ToPath() => $"{FileName}.{Format.AsFileExtension()}";
}

public static class FileFormatExtensions
{
    public static string AsFileExtension(this FileFormat fileFormat) =>
        fileFormat == FileFormat.Json ? "json" : "txt";
}

public enum FileFormat
{
    Json,
    Txt
}

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

        if (ingredients.Count() > 0)
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


public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
    IEnumerable<Ingredient> ReadIngredientsFromUser();
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

    public Ingredient GetById(int id)
    {
        // return All.FirstOrDefault(i => i.Id == id);

        foreach (var ingredient in All)
        {
            if (ingredient.Id == id)
                return ingredient;
        }
        return null;
    }
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
            foreach (var recipe in allRecipes)
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
        foreach (var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    public IEnumerable<Ingredient> ReadIngredientsFromUser()
    {
        var ingredients = new List<Ingredient>();
        bool shallStop = false;

        while (!shallStop)
        {
            Console.WriteLine("Add an ingredient by its ID, or type anything else if finished");

            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int id))
            {
                var selectedIngredient = _ingredientsRegister.GetById(id);
                if (selectedIngredient is not null)
                {
                    ingredients.Add(selectedIngredient);
                }
            }
            else
            {
                shallStop = true;
            }
        }

        return ingredients;
    }
}

public interface IRecipesRepository
{
    List<Recipe> Read(string filepath);
    void Write(List<Recipe> allRecipes, string filepath);
}


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

public interface IStringsRepository
{
    List<string> Read(string filepath);
    void Write(string filepath, List<string> allStrings);
}

public class StringsTextualRepository : IStringsRepository
{
    private static readonly string Separator = Environment.NewLine;

    public List<string> Read(string filepath)
    {
        if (File.Exists(filepath))
        {
            var fileContents = File.ReadAllText(filepath);
            return fileContents.Split(Separator).ToList();
        }
        return new List<string>();
    }

    public void Write(string filepath, List<string> strings)
    {
        File.WriteAllText(filepath, string.Join(Separator, strings));
    }
}

public class StringsJsonRepository : IStringsRepository
{
    public List<string> Read(string filepath)
    {
        if (File.Exists(filepath))
        {
            var fileContents = File.ReadAllText(filepath);
            return JsonSerializer.Deserialize<List<string>>(fileContents);
        }
        return new List<string>();
    }

    public void Write(string filepath, List<string> strings)
    {
        File.WriteAllText(filepath, JsonSerializer.Serialize(strings));
    }
}