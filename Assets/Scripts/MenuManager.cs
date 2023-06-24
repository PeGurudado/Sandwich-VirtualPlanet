using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void PlayButton()
    {       
        CancelInvoke("LoadNextScene");
        Invoke("LoadNextScene",0.25f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Loads next scene (Main scene)
    }
}
