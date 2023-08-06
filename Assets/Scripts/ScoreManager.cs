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

    private void Start() 
    {
        UpdateScoreText();
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        if(currentScore > PlayerPrefs.GetInt(HighscoreDataString,0))
            PlayerPrefs.SetInt(HighscoreDataString,currentScore);

        if(menuHighscoreText) menuHighscoreText.text = "Highscore: "+PlayerPrefs.GetInt(HighscoreDataString,0); 
    }

    private void UpdateScoreText()
    {
        if(scoreText) scoreText.text = "Score "+currentScore;
    }

    public void UpdateGameoverScores()
    {
        UpdateHighscore();
        menuScoreText.text = "Score: "+currentScore;
    }

    public int CalculateScore(Sandwich currentSandwich, List<Ingredient> requiredOrderIngredients)
    {
        int prevScore = currentScore;
        if (currentSandwich.CompoundIngredients.Count <= requiredOrderIngredients.Count)
        {
            for (int i = 0; i < requiredOrderIngredients.Count; i++)
            {
                Ingredient requiredIngredient = requiredOrderIngredients[i];

                if (i < currentSandwich.CompoundIngredients.Count)
                {
                    Ingredient currentIngredient = currentSandwich.CompoundIngredients[i];

                    if (requiredIngredient.IngredientName == currentIngredient.IngredientName)
                    {
                        // Ingredient is correct
                        currentScore += 15;

                        if (i > 0 && requiredIngredient.IngredientName == currentSandwich.CompoundIngredients[i - 1].IngredientName)
                        {
                            // Ingredient is in the proper order
                            currentScore += 10;
                        }
                    }
                    else
                    {
                        // Ingredient is incorrect
                        currentScore -= 5;
                    }
                }
                else
                {
                    // Less ingredients than expected
                    currentScore -= 5;
                }
            }
        }
        else
        {
            // More ingredients than expected
            currentScore -= 5;
        }

        UpdateScoreText();

        int totalScoreBonus = -(prevScore - currentScore);
        return totalScoreBonus; 

    }
}
