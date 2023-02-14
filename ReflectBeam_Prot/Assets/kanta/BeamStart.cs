using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStart : MonoBehaviour
{
    BeamShot beamShot;
    void Start()
    {
        beamShot = GetComponent<BeamShot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        beamShot.RayShot(transform.position, transform.right);
    }
}
