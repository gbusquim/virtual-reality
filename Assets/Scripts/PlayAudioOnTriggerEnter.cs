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
    private GameObject myObject;
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
      myObject = GameObject.Find("testeCylinder");
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
            //myObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
            StartCoroutine(ExampleCoroutine());
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

    IEnumerator ExampleCoroutine()
    {
        
        Color previousColor = myObject.GetComponent<Renderer>().material.color;
        //Print the time of when the function is first called.
        if (previousColor == Color.red)
            myObject.GetComponent<Renderer>().material.color = Color.green;

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds((float) 0.2);

        //After we have waited 5 seconds print the time again.
        myObject.GetComponent<Renderer>().material.color = previousColor;
    }
}
