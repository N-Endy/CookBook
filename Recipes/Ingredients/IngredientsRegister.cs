using Cookbook.Recipes.Ingredients;

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
