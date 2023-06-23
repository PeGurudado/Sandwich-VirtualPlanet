using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private OrderController orderController;

    [Header("Sandwich Managment")]    
    [SerializeField] private Transform tableSandwichParent;
    [SerializeField] private RectTransform plateRectTransform;
    [SerializeField] private IngredientHolder ingredientHolder; 
    private Sandwich currentSandwich;    

    [Header("Gameover Screen")]
    [SerializeField] private GameObject gameoverScreen;

    private bool isGameover;

    private void Start() {
        SetTableSandwich();
    }

    private void SetTableSandwich(){
        currentSandwich = new Sandwich();    
    }

    public void AddIngredientToSandwich(Ingredient selectedIngredient){
        IngredientHolder newIngredient = Instantiate(ingredientHolder,  tableSandwichParent);
        newIngredient.gameObject.SetActive(true);
        newIngredient.Initialize(selectedIngredient);

        currentSandwich.compoundIngredients.Add(selectedIngredient);
    }

    public RectTransform GetPlateRectTransform(){
        if(tableSandwichParent.childCount > 0)
            return tableSandwichParent.GetChild(tableSandwichParent.childCount-1).transform as RectTransform;
        else
            return plateRectTransform;
    }

    public void DeliverSandwichButton(){
        scoreManager.CalculateScore(currentSandwich, orderController.GetRequiredOrderIngredients());
        FreeSandwichPlate();
        orderController.GetRandomSandwichOrder();
        currentSandwich = new Sandwich();
    }
    
    private void FreeSandwichPlate()
    {
        // Destroy all ingredients inside tableSandwichParent
        foreach (Transform child in tableSandwichParent)
        {
            if(child.gameObject.activeInHierarchy)
                Destroy(child.gameObject);
        }
        SetTableSandwich();
    }

    public void GameOver(){
        isGameover = true;
        
        gameoverScreen.SetActive(true);
        scoreManager.UpdateGameoverScores();
    }

    public bool IsGameRunnning(){
        return !isGameover;
    }

    public void RestartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads scene
    }

    public void LoadHomeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Loads menu scene
    }
}