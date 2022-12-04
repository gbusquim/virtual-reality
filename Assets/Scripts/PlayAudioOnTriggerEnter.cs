using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{

    public AudioClip clipOne;
    public AudioClip clipTwo;
    public AudioClip clipThree;

    private AudioSource source;
    private GameObject myObject;
    private GameObject myObject2;
    private GameObject myObject3;
    public InputActionProperty pinchAnimationAction;

    private bool isPressingBackPart = false;
    private bool isHoldingInstrument = false;

    public string hitTag;
    public string grabTag;
    private float triggerValue;

    public bool useVelocity = true;
    public bool updateTime = false;
    public float minVelocity = 0;
    public float maxVelocity = 2;

    public float timer = 0.0f;
    public float openTimer = 0.0f;

    private IEnumerator patternCoroutine;
    private IEnumerator hitCoroutine;

    // Start is called before the first frame update
    void Start() {
        source = GetComponent<AudioSource>(); 
        myObject = GameObject.Find("testeCylinder"); 
        myObject3 = GameObject.Find("testeCylinder3"); 
    }

    // Update is called once per frame
    void Update() {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (triggerValue == 1 && !isPressingBackPart && isHoldingInstrument) {
            source.PlayOneShot(clipThree);
            //StartCoroutine(ExampleCoroutine());
            isPressingBackPart = true;
        }
        else if (triggerValue == 0) {
            isPressingBackPart = false;
        }
        if (updateTime)
            timer += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(grabTag))
            isHoldingInstrument = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(grabTag))
            isHoldingInstrument = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == hitTag) {
            if (triggerValue == 0)
                PlaySoundOnCollision(source, clipOne, other);
            else if (triggerValue == 1)
                PlaySoundOnCollision(source, clipTwo, other);
        }
    }

    private void PlaySoundOnCollision(AudioSource source, AudioClip clip, Collision other) 
    {
        VelocityEstimator estimator = other.collider.GetComponent<VelocityEstimator>();
        if (estimator && useVelocity) {
            float velocity = estimator.GetVelocityEstimate().magnitude;
            float volume = Mathf.InverseLerp(minVelocity, maxVelocity, velocity);
            //Debug.Log(volume.ToString("n2"));
            source.PlayOneShot(clip, volume);
            if (timer - openTimer < 0.2 || timer - openTimer > 0.8) {
                StartCoroutine(HitCoroutine());
            }
            else
            {
                //Debug.Log(timer - (int) timer);
               // Debug.Log(timer - openTimer);
            }

        }
        else {
            source.PlayOneShot(clip);
        }
    }

    public void BeginPattern(int number) {
        //accuracyCoroutine = AccuracyCoroutine(number);
        patternCoroutine = PatternCoroutine(number); 
        updateTime = true;
        StartCoroutine(patternCoroutine);
        //StartCoroutine(accuracyCoroutine);
    }

    public void StopPattern() {
        updateTime = false;
        timer = 0.0f;
        openTimer = 0.0f;
        //StopCoroutine(accuracyCoroutine);
        StopCoroutine(patternCoroutine);
        ChangeColorToGray();
    }

    public IEnumerator HitCoroutine() {
        // ChangeColor2(255);
        yield return new WaitForSeconds((float) 0.4);
        // ChangeColor2(190);
    }
    
    public IEnumerator PatternCoroutine(int levelNumber)
    {
        while (true) {
            switch(levelNumber) 
            {
                case 1:
                    ChangeColorToBlue();
                    openTimer = timer;
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.8);

                    ChangeColorToYellow();
                    openTimer = timer;
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.8);
                    break;

                case 2:
                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.17);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 1.7);
                    break;

                case 3:
                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.33);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.75);
                    break;

                case 4:
                //Primeiro Compasso
                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                //Segundo Compasso
                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 1.7);
                    break;

                case 5:
                //Primeiro Compasso
                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                //Segundo Compasso
                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlue();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToBlues();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);

                    ChangeColorToYellow();
                    yield return new WaitForSeconds((float) 0.16);
                    ChangeColorToGray();

                    yield return new WaitForSeconds((float) 0.31);
                    break;
            }   
        }
    }
    
    private void ChangeColorToGray() {
        myObject.GetComponent<Renderer>().material.color = new Color32(190, 190, 190, 255);
    }

    private void ChangeColorToBlue() {
        myObject.GetComponent<Renderer>().material.color = new Color32(0, 0, 133, 255);
    }

    private void ChangeColorToYellow() {
        myObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 87, 255);
    }

    private void ChangeColor3(int colorValue) {
        //myObject3.GetComponent<Renderer>().material.color = new Color32(190, 190, (byte)colorValue, 255);
    }

}
