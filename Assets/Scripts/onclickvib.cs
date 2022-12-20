using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onclickvib : MonoBehaviour
{
    public Button triggerButton;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume=0.5f;

    void Update()
    {
        Button btn = triggerButton.GetComponent<Button>();
        btn.onClick.AddListener(VibrateOnClick);
        btn.onClick.AddListener(SoundOnClick);
    }

    // Update is called once per frame
    public void VibrateOnClick()
    {
        Handheld.Vibrate();
    }

    public void SoundOnClick()
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
