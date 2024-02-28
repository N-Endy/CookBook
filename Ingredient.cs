namespace Cookbook
{
    interface IPreparation
    {
        public void Preparation();
    }

    public class Ingredients : IPreparation
    {
        Ingredients ingredients = new();
        public int Id { get; }
        public string Name { get; }

        

        public virtual void Preparation()
        {
        }

        public void PrintIngredient()
        {
            Console.WriteLine($"{Id}. {Name}");
        }
    }

    public class WheatFlour : Ingredients
    {
        public new int Id => 1;
        public new string Name => "Wheat Flour";

        public override void Preparation() => Console.WriteLine("Sieve. Add to other ingredients");
    }

    public class CoconutFlour : Ingredients
    {
        public new int Id => 2;
        public new string Name => "Coconut Flour";

        public override void Preparation() => Console.WriteLine("Sieve. Add to other ingredients");
    }

    public class Butter : Ingredients
    {
        public new int Id => 3;
        public new string Name => "Butter";

        public override void Preparation() => Console.WriteLine("Melt on low heat. Add to other ingredients");
    }
}