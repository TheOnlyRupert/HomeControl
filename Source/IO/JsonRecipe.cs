using System.Collections.ObjectModel;

namespace HomeControl.Source.IO;

public class JsonRecipe {
    public ObservableCollection<Recipe> recipeList { get; set; }
}

public class Recipe {
    public string name { get; set; }
    public string minutes { get; set; }
    public string tags { get; set; }
    public string n_steps { get; set; }
    public string steps { get; set; }
    public string description { get; set; }
    public string ingredients { get; set; }
    public string n_ingredients { get; set; }
}