using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StopAudio : MonoBehaviour
{
    private AudioSource source;
    public Sprite playImage;
    public Button button;

    void OnEnable()
    {      
        source = GetComponent<AudioSource>();
        source.Stop();
        source.loop = false;
        button.GetComponent<Image>().sprite = playImage;
    }
}
