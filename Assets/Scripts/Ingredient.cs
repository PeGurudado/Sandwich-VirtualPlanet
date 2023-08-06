using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public IngredientNames IngredientName; 
    public Sprite IngredientIcon;
}

public enum IngredientNames{
    Bread, Lettuce, Ham, Cheese, Tomatoes, Onions, 
    Potatoes, Bacon, Peperoni, Cucumber, Mushrooms, Spinach
}