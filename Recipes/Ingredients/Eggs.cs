namespace Cookbook.Recipes.Ingredients;
public class Eggs : Ingredient
{
    public override int Id => 7;
    public override string Name => "Eggs";
    public override string PreparationInstructions =>
        $"Whip eggs properly. {base.PreparationInstructions}";
}