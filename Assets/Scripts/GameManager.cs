using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private OrderController orderController;

    [Header("Sandwich Management")]    
    [SerializeField] private Transform tableSandwichParent;
    [SerializeField] private RectTransform plateRectTransform;
    [SerializeField] private IngredientHolder ingredientHolder; 
    private Sandwich currentSandwich;    

    [Header("Gameover Screen")]
    [SerializeField] private GameObject gameoverScreen;

    private bool isGameover, hasStarted;

    [Header("Audio")]
    [SerializeField] private AudioClip addIngredientAudioClip;
    [SerializeField] private AudioClip removeIngredientAudioClip;

    [SerializeField] private AudioSource audioSource;

    private void Start() 
    {
        SetTableSandwich();
    }

    private void SetTableSandwich()
    {
        currentSandwich = new Sandwich();    
    }

    public Sandwich GetSandwich()
    {
        return currentSandwich;
    }

    public void AddIngredientToSandwich(Ingredient selectedIngredient){
        IngredientHolder newIngredient = Instantiate(ingredientHolder,  tableSandwichParent);
        newIngredient.gameObject.SetActive(true);
        newIngredient.Initialize(selectedIngredient);

        currentSandwich.CompoundIngredients.Add(selectedIngredient);

        PlaySoundClip(addIngredientAudioClip);
    }

    public void RemoveSandwichIngredientFromTop()
    {
        Destroy(tableSandwichParent.GetChild(tableSandwichParent.childCount-1).gameObject);
        currentSandwich.CompoundIngredients.RemoveAt(currentSandwich.CompoundIngredients.Count - 1);

        PlaySoundClip(removeIngredientAudioClip);
    }

    private void PlaySoundClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public RectTransform GetTopRectTransform()
    {
        if(tableSandwichParent.childCount > 0)
            return tableSandwichParent.GetChild(tableSandwichParent.childCount-1).transform as RectTransform;
        else
            return plateRectTransform;
    }

    public void DeliverSandwichButton()
    {
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

    public void GameOver()
    {
        isGameover = true;
        
        gameoverScreen.SetActive(true);
        scoreManager.UpdateGameoverScores();
    }

    public void StartGame(){
        hasStarted = true;
    }

    public bool IsGameRunnning()
    {
        return !isGameover && hasStarted;
    }

    public void RestartSceneButton()
    {
        CancelInvoke("RestartScene");
        Invoke("RestartScene", 0.25f);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads scene
    }

    public void LoadHomeScene()
    {
        CancelInvoke("LoadHomeSceneButton");
        Invoke("LoadHomeSceneButton",0.25f);
    }

    public void LoadHomeSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Loads menu scene
    }
}