using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTp;
    public GameObject rightTp;

    // Update is called once per frame
    void Update()
    {
        leftTp.SetActive(false);
        rightTp.SetActive(false);
    }
}
