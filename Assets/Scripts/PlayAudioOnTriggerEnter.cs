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
    private GameObject tamborim;
    public InputActionProperty pinchAnimationAction;

    private bool isPressingBackPart = false;
    private bool isHoldingInstrument = false;

    public string hitTag;
    public string grabTag;
    private float triggerValue;

    public bool useVelocity = true;
    private bool updateTime = false;
    public float minVelocity = 0;
    public float maxVelocity = 2;

    private float timer = 0.0f;
    private float openTimer = 0.0f;
    private int levelNumber = 0;

    private IEnumerator patternCoroutine;
    private IEnumerator hitCoroutine;

    // Start is called before the first frame update
    void Start() {
        source = GetComponent<AudioSource>(); 
        tamborim = GameObject.Find("Tamborim 0"); 
    }

    // Update is called once per frame
    void Update() {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (triggerValue == 1 && !isPressingBackPart && isHoldingInstrument) {
            source.PlayOneShot(clipThree);
            checkCorrectHit();
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
            source.PlayOneShot(clip, volume);
            Debug.Log(levelNumber);
            Debug.Log(timer - openTimer);
            checkCorrectHit();
        }
        else {
            source.PlayOneShot(clip);
        }
    }

    public void BeginPattern(int number) {
        patternCoroutine = PatternCoroutine(number);
        levelNumber = number; 
        updateTime = true;
        StartCoroutine(patternCoroutine);
    }

    public void StopPattern() {
        updateTime = false;
        timer = 0.0f;
        openTimer = 0.0f;
        StopCoroutine(patternCoroutine);
        ChangeColorToGray();
    }

    public IEnumerator HitCoroutine() {
        ChangeColorToGreen();
        yield return new WaitForSeconds((float) 0.4);
        ChangeColorToGray();
    }

    public void checkCorrectHit() {
        if (levelNumber == 1 && (timer - openTimer < 0.2 || timer - openTimer > 0.8)) {
            StartCoroutine(HitCoroutine());
        }
    }
    
    public IEnumerator PatternCoroutine(int levelNumber)
    {
        for(int i = 0; i < 2; i++) {
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
                    break;
            }   
        }
    }
    
    private void ChangeColorToGray() {
        tamborim.GetComponent<Renderer>().material.color = new Color32(147, 139, 139, 255);
    }

    private void ChangeColorToBlue() {
        tamborim.GetComponent<Renderer>().material.color = new Color32(19, 22, 137, 255);
    }

    private void ChangeColorToYellow() {
        tamborim.GetComponent<Renderer>().material.color = new Color32(141, 145, 45, 255);
    }

    private void ChangeColorToGreen() {
        tamborim.GetComponent<Renderer>().material.color = new Color32(9, 104, 21, 255);
    }
}