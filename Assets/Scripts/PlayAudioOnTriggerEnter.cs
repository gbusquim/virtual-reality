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
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        Debug.Log(hitTag);
        Debug.Log(other.collider.tag == grabTag);
        if (other.collider.tag == hitTag) {
            if (triggerValue == 0)
                source.PlayOneShot(clipOne);
            else if (triggerValue == 1)
                source.PlayOneShot(clipTwo);
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
