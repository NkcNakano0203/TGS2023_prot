using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflectTest : MonoBehaviour,IRayRecevier
{
    
    BeamShot beamShot;



    public LastHit Hit(Vector3 rayVec, Vector3 rayPos)
    {

        throw new System.NotImplementedException();
    }



    void Start()
    {
        beamShot = GetComponent<BeamShot>();
    }
    // Update is called once per frame
    void Update()
    {
        if()
    }
    
}
