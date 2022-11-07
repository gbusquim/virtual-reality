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

    public string hitTag;
    public string grabTag;
    private float triggerValue;

    public bool useVelocity = true;
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
            if (triggerValue == 0)
                PlaySoundOnCollision(source, clipOne, other);
            else if (triggerValue == 1)
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
            Debug.Log(volume.ToString("n2"));
            source.PlayOneShot(clip, volume);
        }
        else {
            source.PlayOneShot(clip);
        }
    } 

    void Update() {
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if (triggerValue == 1 && !isPressingBackPart && isHoldingInstrument) {
            source.PlayOneShot(clipThree);
            isPressingBackPart = true;
        }
        else if(triggerValue == 0) {
            isPressingBackPart = false;
        }
    }
}
