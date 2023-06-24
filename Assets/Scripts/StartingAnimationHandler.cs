using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAnimationHandler : MonoBehaviour
{
    [SerializeField] private GameObject topFrameObj;
    [SerializeField] private TimerController timerController;
    [SerializeField] private GameManager gameManager;

    private void EnableTopFrame()
    {
        topFrameObj.SetActive(true);
    }

    private void EnableTimer()
    {
        timerController.InitializeTimer();
    }

    private void TriggerEndOfAnimation()
    {
        EnableTopFrame();
        EnableTimer();
        gameManager.StartGame();
    }
}
