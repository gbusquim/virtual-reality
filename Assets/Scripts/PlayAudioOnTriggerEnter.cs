using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{

    public AudioClip clipOne;
    public AudioClip clipTwo;
    public AudioClip clipThree;

    private AudioSource source;
    public InputActionProperty pinchAnimationAction;

    private bool isPressingBackPart = false;
    private bool isHoldingInstrument = false;

    private float previousTriggerValue = 0;

    public string hitTag;
    public string grabTag;
    private float triggerValue;

    public bool useVelocity = false;
    public float minVelocity = 0;
    public float maxVelocity = 2;

    // Start is called before the first frame update
    void Start() {
      source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == hitTag) {
            if (!isPressingBackPart)
                PlaySoundOnCollision(source, clipOne, other);
            else
                PlaySoundOnCollision(source, clipTwo, other);
        }
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

    private void PlaySoundOnCollision(AudioSource source, AudioClip clip, Collision other) 
    {
        VelocityEstimator estimator = other.collider.GetComponent<VelocityEstimator>();
        if (estimator && useVelocity) {
            float velocity = estimator.GetVelocityEstimate().magnitude;
            float volume = Mathf.InverseLerp(minVelocity, maxVelocity, velocity);

            source.PlayOneShot(clip, volume);
        }
        else {
            source.PlayOneShot(clip);
        }
    } 

    void Update() {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        Debug.Log(triggerValue);
        Debug.Log(isPressingBackPart);
        if (triggerValue > 0.5 && !isPressingBackPart && isHoldingInstrument) {
            source.PlayOneShot(clipThree);
            isPressingBackPart = true;
            previousTriggerValue = triggerValue; 
        }
        else if(triggerValue < previousTriggerValue) {
            isPressingBackPart = false;
        }
    }
}
