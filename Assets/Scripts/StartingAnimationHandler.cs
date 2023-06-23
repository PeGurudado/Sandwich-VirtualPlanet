using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingAnimationHandler : MonoBehaviour
{
    [SerializeField] private GameObject topFrameObj;
    [SerializeField] private TimerController timerController;

    public void EnableTopFrame(){
        topFrameObj.SetActive(true);
    }

    public void EnableTimer(){
        timerController.InitializeTimer();
    }
}
