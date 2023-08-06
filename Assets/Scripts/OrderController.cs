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
    [SerializeField] private IngredientHolder ingredientOrderPlate;

    [SerializeField] private Transform orderIngredientsPlateParent;

    [SerializeField] private List<Ingredient> AllListedIngredients;
    [SerializeField] private Ingredient breadIngredient;

    // private Sandwich[] sandwiches; // Array to store all the available sandwiches
    private string scriptableObjectsPath = "Sandwiches"; 

    private List<Ingredient> requiredOrderIngredients;

    [SerializeField] private int minIngredients = 2, maxIngredients = 5, ingredientsLimit = 20;

    private void Awake() 
    {
        // Load all the sandwiches from the "Sandwiches" folder in the Resources folder
        // sandwiches = Resources.LoadAll<Sandwich>(scriptableObjectsPath);
    }

    public Sandwich CreateSandwichCombination()
    {
        int ingredientsAmount = Random.Range(minIngredients, maxIngredients);

        Sandwich newSandwich = new Sandwich();
        newSandwich.CompoundIngredients.Add(breadIngredient);// Adds a bread at begin of sandwich


        for (int i = 0; i < ingredientsAmount; i++)
        {
            int randomIndex = Random.Range(0, AllListedIngredients.Count);

            Ingredient randomIngredient = AllListedIngredients[randomIndex];
            newSandwich.CompoundIngredients.Add(randomIngredient);
        }

        newSandwich.CompoundIngredients.Add(breadIngredient); //Adds a bread at end of sandwich

        //After each sandwich combination, the next will be with more ingredients
        if (maxIngredients < ingredientsLimit)
        {
            minIngredients++;
            maxIngredients++;
        }

        return newSandwich;
    }

    private void UpdateOrderPlateIcon(){
        // Destroy all old ingredients inside plate
        foreach (Transform child in orderIngredientsPlateParent)
        {
            if(child.gameObject.activeInHierarchy)
                Destroy(child.gameObject);
        }
        //Add new ingredients
        foreach (var ingredient in requiredOrderIngredients)
        {
            IngredientHolder newIngredient = Instantiate(ingredientOrderPlate,  orderIngredientsPlateParent);
            newIngredient.gameObject.SetActive(true);
            newIngredient.Initialize(ingredient);
        }
    }

    private void Start()
    {        
        GetRandomSandwichOrder();
    }    

    public void GetRandomSandwichOrder()
    {
        // Get a random sandwich from the loaded sandwiches
        Sandwich randomSandwich = CreateSandwichCombination(); //sandwiches[Random.Range(0, sandwiches.Length)];

        // Access the properties of the random sandwich
        // string sandwichName = randomSandwich.SandwichName;
        // Sprite sandwichIcon = randomSandwich.SandwichIcon;
        requiredOrderIngredients = randomSandwich.CompoundIngredients;        

        // orderIcon.sprite = sandwichIcon;
        // orderName.text = sandwichName;

        SetOrderIngredientsUI();
    }

    private void SetOrderIngredientsUI()
    {
        UpdateOrderPlateIcon();

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
