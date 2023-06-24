using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sandwich", menuName = "Sandwich")]
public class Sandwich : ScriptableObject
{
    public string SandwichName;
    public Sprite SandwichIcon; 
    public List<Ingredient> CompoundIngredients = new List<Ingredient>();
    
}