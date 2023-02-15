using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflectTest : MonoBehaviour,IRayRecevier
{
    
    BeamShot beamShot;
    Vector3 dir;
    Vector3 shotPos;


    public LastHit Hit(Vector3 rayVec, Vector3 rayPos)
    {
        dir = rayVec;
        shotPos = rayPos;
        throw new System.NotImplementedException();
    }



    void Start()
    {
        beamShot = GetComponent<BeamShot>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Dot(transform.right,-dir)>=0)
        {
            beamShot.RayShot(shotPos,Vector3.Reflect(dir,transform.right ));
        }
        else
        {
            beamShot.RayShot(shotPos,Vector3.Reflect( dir,-transform.right) );
        }
    }
    
}
