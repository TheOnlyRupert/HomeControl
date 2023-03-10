namespace HomeControl.Source.ModulesTesting.TamagotchiTests;

public class FoodItem {
    public string name { get; set; }

    /* controls the rate of which a food item goes bad. 
       0 means the food will instantly go bad. 
       Higher number increase decay rate of item. */
    public float decayRate { get; set; }

    /* Uses a random number generator using the ageRange as min/max */
    public int[] startingAgeRange { get; set; }

    /* controls how temperature effects the rate of which a food item goes bad. */
    public float temperatureFactorFrozen { get; set; }
}

public class FoodItemActive {
    public FoodItem foodItem { get; set; }
    public float age { get; set; }
    public float temperature { get; set; }
}