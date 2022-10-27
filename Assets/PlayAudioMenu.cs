using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayAudioMenu : MonoBehaviour
{
    public AudioClip clipThree;
    private AudioSource source;

    public void PlayAudio() {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clipThree);

    }
}
