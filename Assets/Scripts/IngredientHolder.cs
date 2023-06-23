using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientHolder : MonoBehaviour
{
    [SerializeField] private Image ingredientImage;
    [SerializeField] private Ingredient ingredient;

    public void Initialize(Ingredient ingredient){
        this.ingredient = ingredient;
        ingredientImage.sprite = ingredient.ingredientIcon;
    }

}
