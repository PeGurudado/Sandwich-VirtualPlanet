using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;
    Button button;

    private void Awake() {
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
