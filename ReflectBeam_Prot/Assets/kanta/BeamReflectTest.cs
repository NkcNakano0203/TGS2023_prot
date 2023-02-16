using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamReflectTest : MonoBehaviour,IRayRecevier
{
    
    BeamShot beamShot;
    Vector3 dir;
    Vector3 shotPos;
    bool hit=false;

    public LastHit Hit(Vector3 rayVec, Vector3 rayPos,RaycastHit a)
    {
        hit = true;
        dir = rayVec;
        shotPos = rayPos;
        if (hit)
        {
            if (Vector3.Dot(transform.right, -dir) > 0)
            {
               // beamShot.RayShot(shotPos, Vector3.Reflect(dir, transform.right));
            }
            else if (Vector3.Dot(transform.right, -dir) < 0)
            {
               // beamShot.RayShot(shotPos, Vector3.Reflect(dir, -transform.right));
            }
            hit = false;
        }
        throw new System.NotImplementedException();
    }



    void Start()
    {
        beamShot = GetComponent<BeamShot>();
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    
    
}
