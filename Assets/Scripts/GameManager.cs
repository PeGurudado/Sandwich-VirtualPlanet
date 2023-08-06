using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private float increaseTimerFromPerfect = 10;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private OrderController orderController;
    [SerializeField] private GradeEffectScript gradeEffect;
    [SerializeField] private GradeEffectScript plusTimerEffect;
    [SerializeField] private TimerController timerController;

    [Header("Sandwich Management")]
    [SerializeField] private AdjustRectBasedOnIngredients adjustOrderScript;
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
        if(tableSandwichParent.childCount > 1)
            return tableSandwichParent.GetChild(tableSandwichParent.childCount-1).transform as RectTransform;
        else
            return plateRectTransform;
    }

    public void IncreaseTimer(float value)
    {
        timerController.IncreaseTimer(value);
    }

    public void DeliverSandwichButton()
    {
        CalculateGrade();

        FreeSandwichPlate();
        orderController.GetRandomSandwichOrder();
        adjustOrderScript.AdjustRectOnIngredients();
        currentSandwich = new Sandwich();
    }
    
    private void CalculateGrade()
    {
        int totalBonus = scoreManager.CalculateScore(currentSandwich, orderController.GetRequiredOrderIngredients()) / orderController.GetRequiredOrderIngredients().Count;

        GradeEffectScript newGradeEffect = Instantiate(gradeEffect, GetTopRectTransform().position, Quaternion.identity, plateRectTransform);
        newGradeEffect.Initialize(totalBonus);

        if (totalBonus >= 10) // In case got a perfect
        {
            Vector3 effectHeightOffset = new Vector2(0, 80);
            IncreaseTimer(increaseTimerFromPerfect);
            GradeEffectScript plusEffect = Instantiate(plusTimerEffect, GetTopRectTransform().position + effectHeightOffset, Quaternion.identity, plateRectTransform);
            plusEffect.Initialize("+"+ increaseTimerFromPerfect);
        }
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