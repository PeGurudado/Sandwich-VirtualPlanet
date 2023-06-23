using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private float totalTime = 120f;  // Total time for the timer in seconds
    [SerializeField] private TextMeshProUGUI timerText;  // Reference to the TMPro text component

    private float currentTime;  // Current time of the timer

    public void InitializeTimer(){
        currentTime = totalTime;
    }

    private void Update()
    {
        if (currentTime > 1)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 1)
            {
                TimerEnded();
            }
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timerString = "Timer "+string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timerString;
    }

    private void TimerEnded()
    {
        gameManager.GameOver();
    }
}