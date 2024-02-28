using Cookbook;

var cookiesRecipesApp = new CookiesRecipesApp(
    new RecipesRepository(),
    new RecipesConsoleUserInteraction());
    
cookiesRecipesApp.Run();