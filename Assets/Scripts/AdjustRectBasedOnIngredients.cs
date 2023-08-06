using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustRectBasedOnIngredients : MonoBehaviour
{
    [SerializeField] RectTransform adjustRect;
    [SerializeField] Transform ingredientsParent;
    [SerializeField] float minIngredientsHeight = 63, baseHeight = 220, ingredientHeight = 5;

    private float rectTopDistance = 25;

    public void AdjustRectOnIngredients()
    {
        var totalIngredientsHeight= ingredientsParent.childCount * ingredientHeight;

        //if(totalIngredientsHeight > minIngredientsHeight)
            adjustRect.sizeDelta = new Vector2(adjustRect.rect.size.x, baseHeight + totalIngredientsHeight);

        AdjustPosition();
    }
    
    private void AdjustPosition()
    {
        adjustRect.anchoredPosition = new Vector3(adjustRect.anchoredPosition.x, (-adjustRect.sizeDelta.y/2) - rectTopDistance );
    }
}
