using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIngredientsRemover : MonoBehaviour
{

    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private IngredientHolder ingredientHolderPrefab;
    private IngredientHolder currentIngredientHolder;
    private bool isDragging = false;
    private Ingredient latestIngredient;

    private void Update()
    {
        if(!gameManager.IsGameRunnning()){
            if(currentIngredientHolder)
            {
                // Destroy the ingredient holder
                Destroy(currentIngredientHolder.gameObject);
                currentIngredientHolder = null;
            }
            return;
        } 

        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over this ingredient bucket
            if (IsMouseOverPlate())
            {
                isDragging = true;
                latestIngredient = GetLastIngredient();
                
                if(latestIngredient != null) 
                {
                    // Create a new ingredient holder and attach it to the mouse position
                    currentIngredientHolder = Instantiate(ingredientHolderPrefab, Input.mousePosition, Quaternion.identity, canvasTransform);
                    currentIngredientHolder.Initialize(latestIngredient);

                    gameManager.RemoveSandwichIngredientFromTop();
                }
            }
        }

        if(currentIngredientHolder == null) return;

        if (isDragging)
        {
            // Update the position of the ingredient holder based on the mouse position
            currentIngredientHolder.transform.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) || !gameManager.IsGameRunnning())
        {
            if (isDragging)
            {
                isDragging = false;

                // Check if the release position is within the plate's bounds
                RectTransform plateRectTransform = gameManager.GetTopRectTransform();
                Vector2 localMousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(plateRectTransform, Input.mousePosition, null, out localMousePos);

                if (IsMouseOverPlate())
                {
                    // Add the ingredient to the sandwich
                    gameManager.AddIngredientToSandwich(latestIngredient);
                }

                // Destroy the ingredient holder
                Destroy(currentIngredientHolder.gameObject);
                currentIngredientHolder = null;
            }
        }
    }

    Ingredient GetLastIngredient()
    {
        List<Ingredient> ingredientsList = gameManager.GetSandwich().CompoundIngredients;    

        if(ingredientsList.Count > 0) return ingredientsList[ingredientsList.Count - 1];

        return null;
    }

    private bool IsMouseOverPlate()
    {
        // Get the RectTransform of the ingredient bucket
        RectTransform bucketRectTransform = gameManager.GetTopRectTransform();

        // Convert the mouse position to the local position of the bucket
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bucketRectTransform, Input.mousePosition, null, out localMousePos);

        // Check if the local mouse position is within the bounds of the bucket
        return bucketRectTransform.rect.Contains(localMousePos);
    }

}

