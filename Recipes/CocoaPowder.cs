namespace Cookbook.Recipes;
public class CocoaPowder : Ingredient
{
    public override int Id => 9;
    public override string Name => "Cocoa Powder";
    public override string PreparationInstructions =>
        $"{base.PreparationInstructions}";
}