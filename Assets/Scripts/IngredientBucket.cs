using UnityEngine;

public class IngredientBucket : MonoBehaviour
{
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private IngredientHolder ingredientHolderPrefab;
    [SerializeField] private Ingredient ingredient; // The specific ingredient for this bucket

    private IngredientHolder currentIngredientHolder;

    private bool isDragging = false;

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
            if (IsMouseOverBucket())
            {
                isDragging = true;

                // Create a new ingredient holder and attach it to the mouse position
                currentIngredientHolder = Instantiate(ingredientHolderPrefab, Input.mousePosition, Quaternion.identity, canvasTransform);
                currentIngredientHolder.Initialize(ingredient);
            }
        }

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
                RectTransform plateRectTransform = gameManager.GetPlateRectTransform();
                Vector2 localMousePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(plateRectTransform, Input.mousePosition, null, out localMousePos);

                if (plateRectTransform.rect.Contains(localMousePos))
                {
                    // Add the ingredient to the sandwich
                    gameManager.AddIngredientToSandwich(ingredient);
                }

                // Destroy the ingredient holder
                Destroy(currentIngredientHolder.gameObject);
                currentIngredientHolder = null;
            }
        }
    }

    private bool IsMouseOverBucket()
    {
        // Get the RectTransform of the ingredient bucket
        RectTransform bucketRectTransform = GetComponent<RectTransform>();

        // Convert the mouse position to the local position of the bucket
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bucketRectTransform, Input.mousePosition, null, out localMousePos);

        // Check if the local mouse position is within the bounds of the bucket
        return bucketRectTransform.rect.Contains(localMousePos);
    }

}