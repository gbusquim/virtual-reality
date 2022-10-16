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

    // Start is called before the first frame update
    void Start() {
      source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.CompareTag(hitTag));
        if (other.CompareTag(hitTag)) {
            if (triggerValue == 0)
                source.PlayOneShot(clipOne);
            else if (triggerValue == 1)
                source.PlayOneShot(clipTwo);
        }
        else if (other.CompareTag(grabTag)) {
            isHoldingInstrument = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(grabTag))
            isHoldingInstrument = false;
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
