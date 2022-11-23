using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayAudioMenu : MonoBehaviour
{
    public AudioClip clipThree;
    private AudioSource source;
    private GameObject tamborim;
    private IEnumerator coroutine;
    private bool paused = false;

    public Button button;
    public Sprite playImage;
    public Sprite pauseImage;

    public void PlayAudio() {

        tamborim = GameObject.Find("testeCylinder");
        source = GetComponent<AudioSource>();
        coroutine = ShowPattern();

        if (source.loop == false) {
            source.clip = clipThree;
            source.loop = true;
            button.GetComponent<Image>().sprite = pauseImage;
            source.Play();
            paused = false;
            StartCoroutine(coroutine);
        }
        else {
            source.loop = false;
            button.GetComponent<Image>().sprite = playImage;
            source.Stop();
            paused = true;
            StopCoroutine(coroutine);
        }
    }

    public IEnumerator ShowPattern()
    {
        while (!paused)
        {
            ChangeColor(Color.red);
            yield return new WaitForSeconds((float) 0.2);
            ChangeColor(Color.black);


            yield return new WaitForSeconds((float) 0.8);

            ChangeColor(Color.red);
            yield return new WaitForSeconds((float) 0.2);
            ChangeColor(Color.black);

            yield return new WaitForSeconds((float) 0.3);
        
            ChangeColor(Color.red);
            yield return new WaitForSeconds((float) 0.2);
            ChangeColor(Color.black);

            yield return new WaitForSeconds((float) 0.3);

            ChangeColor(Color.red);
            yield return new WaitForSeconds((float) 0.2);
            ChangeColor(Color.black);

            yield return new WaitForSeconds((float) 1.8);
        }

    }

    private void ChangeColor(Color chosenColor) {
        tamborim.GetComponent<Renderer>().material.color = chosenColor;
    }

    private IEnumerator ShowHit() {
        ChangeColor(Color.red);
        yield return new WaitForSeconds((float) 0.2);
        ChangeColor(Color.black);
    }

    void Update() {
        if(paused) {
            StopCoroutine(coroutine);
        }
    }

}
