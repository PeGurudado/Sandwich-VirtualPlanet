using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderController : MonoBehaviour
{
    [SerializeField] private Image orderIcon;
    [SerializeField] private TextMeshProUGUI orderName;

    [SerializeField] private Transform orderIngredientsParent;
    [SerializeField] private IngredientHolder ingredientOrderHolder;

    private Sandwich[] sandwiches; // Array to store all the available sandwiches
    private string scriptableObjectsPath = "Sandwiches"; 

    private List<Ingredient> requiredOrderIngredients;

    private void Awake() 
    {
        // Load all the sandwiches from the "Sandwiches" folder in the Resources folder
        sandwiches = Resources.LoadAll<Sandwich>(scriptableObjectsPath);
    }

    private void Start()
    {        
        GetRandomSandwichOrder();
    }    

    public void GetRandomSandwichOrder()
    {
        // Get a random sandwich from the loaded sandwiches
        Sandwich randomSandwich = sandwiches[Random.Range(0, sandwiches.Length)];

        // Access the properties of the random sandwich
        string sandwichName = randomSandwich.SandwichName;
        Sprite sandwichIcon = randomSandwich.SandwichIcon;
        requiredOrderIngredients = randomSandwich.CompoundIngredients;        

        orderIcon.sprite = sandwichIcon;
        orderName.text = sandwichName;

        SetOrderIngredientsUI();
    }

    private void SetOrderIngredientsUI()
    {

        // Destroy all ingredients inside orderIngredientsParent
        foreach (Transform child in orderIngredientsParent)
        {
            if(child.gameObject.activeInHierarchy)
                Destroy(child.gameObject);
        }

        foreach (var ingredient in requiredOrderIngredients)
        {
            IngredientHolder newIngredient = Instantiate(ingredientOrderHolder, orderIngredientsParent);
            newIngredient.Initialize(ingredient);
            newIngredient.gameObject.SetActive(true);
        }
    }

    public List<Ingredient> GetRequiredOrderIngredients()
    {
        return requiredOrderIngredients;
    }
}
