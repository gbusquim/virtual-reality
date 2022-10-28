using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayAudioMenu : MonoBehaviour
{
    public AudioClip clipThree;
    private AudioSource source;

    public Button button;
    public Sprite playImage;
    public Sprite pauseImage;

    public void PlayAudio() {

        source = GetComponent<AudioSource>();
        if (source.loop == false) {
            source.clip = clipThree;
            source.loop = true;
            button.GetComponent<Image>().sprite = pauseImage;
            source.Play();
        }
        else {
            source.loop = false;
            button.GetComponent<Image>().sprite = playImage;
            source.Stop();
        }
    }
}
