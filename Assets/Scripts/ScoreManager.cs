using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI menuHighscoreText;
    [SerializeField] private TextMeshProUGUI menuScoreText;
    private int currentScore = 0;

    public static string HighscoreDataString = "Highscore";

    private void Start() {
        UpdateScoreText();
        UpdateHighscore();
    }

    private void UpdateHighscore(){
        if(currentScore > PlayerPrefs.GetInt(HighscoreDataString,0))
            PlayerPrefs.SetInt(HighscoreDataString,currentScore);

        if(menuHighscoreText) menuHighscoreText.text = "Highscore: "+PlayerPrefs.GetInt(HighscoreDataString,0); 
    }

    private void UpdateScoreText(){
        if(scoreText) scoreText.text = "Score "+currentScore;
    }

    public void UpdateGameoverScores(){
        UpdateHighscore();
        menuScoreText.text = "Score: "+currentScore;
    }

    public void CalculateScore(Sandwich currentSandwich, List<Ingredient> requiredOrderIngredients)
    {
        if (currentSandwich.compoundIngredients.Count <= requiredOrderIngredients.Count)
        {
            int correctIngredientsCount = 0;

            for (int i = 0; i < requiredOrderIngredients.Count; i++)
            {
                Ingredient currentIngredient = requiredOrderIngredients[i];

                if (i < currentSandwich.compoundIngredients.Count)
                {
                    Ingredient sandwichIngredient = currentSandwich.compoundIngredients[i];

                    if (currentIngredient.ingredientName == sandwichIngredient.ingredientName)
                    {
                        // Ingredient is correct
                        currentScore += 10;

                        if (i > 0 && currentIngredient.ingredientName == currentSandwich.compoundIngredients[i - 1].ingredientName)
                        {
                            // Ingredient is in the proper order
                            currentScore += 5;
                        }

                        correctIngredientsCount++;
                    }
                    else
                    {
                        // Ingredient is incorrect
                        currentScore -= 5;
                    }
                }
                else
                {
                    // Extra ingredient in the sandwich
                    currentScore -= 5;
                }
            }

            // Add additional scores for each correct ingredient placed
            currentScore += correctIngredientsCount * 5;
        }
        else
        {
            // Incorrect number of ingredients
            currentScore -= 5;
        }

        UpdateScoreText();
    }
}
