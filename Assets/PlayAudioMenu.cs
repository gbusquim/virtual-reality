using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayAudioMenu : MonoBehaviour
{

    private AudioSource source;
    private GameObject tamborim;
    private IEnumerator coroutine;

    public Button button;
    public Sprite playImage;
    public Sprite pauseImage;
    public AudioClip audioClip;
    public int levelNumber;

    PlayAudioOnTriggerEnter musicInstrument;

    public void PlayAudio() {
        musicInstrument = GameObject.Find("testeCylinder").GetComponent<PlayAudioOnTriggerEnter>();
        source = GetComponent<AudioSource>();

        if (source.loop == false) {
            source.clip = audioClip;
            source.loop = true;
            button.GetComponent<Image>().sprite = pauseImage;
            source.Play();
            musicInstrument.BeginPattern(levelNumber);
        }
        else {
            source.loop = false;
            button.GetComponent<Image>().sprite = playImage;
            source.Stop();
            musicInstrument.StopPattern();
        }
    }

}
