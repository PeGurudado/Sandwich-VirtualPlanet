using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sandwich", menuName = "Sandwich")]
public class Sandwich : ScriptableObject
{
    public string sandwichName;
    public Sprite sandwichIcon;
    public List<Ingredient> compoundIngredients = new List<Ingredient>();
    
}