using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour,IRayRecevier
{
    
    bool beamHit = false;
    public LastHit RayEnter(Vector3 rayVec, Vector3 rayPos)
    {        
        beamHit = true;
        return new LastHit(this.gameObject.gameObject);
    }
    


    public void RayExit()
    {
         beamHit = false;          
    } 

    
    public bool GetBeamHit() { return beamHit; }
}
