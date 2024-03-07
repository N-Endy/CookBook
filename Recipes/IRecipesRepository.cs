using Cookbook.Recipes;

public interface IRecipesRepository
{
    List<Recipe> Read(string filepath);
    void Write(List<Recipe> allRecipes, string filepath);
}
