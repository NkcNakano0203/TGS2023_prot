using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStart : MonoBehaviour
{
    BeamShot beamShot;
    [SerializeField]
    BeamDrow beamDrow;
    void Start()
    {
        beamShot = GetComponent<BeamShot>();

    }

    // Update is called once per frame
    void Update()
    {
       Vector3 endPos =  beamShot.RayShot(transform.position, transform.right);
        beamDrow.DrawLine( transform.position, endPos, true);
    }
}
