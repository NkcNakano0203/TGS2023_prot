using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour,IRayRecevier
{
    
    bool beamHit = false;
    public LastHit Hit(Vector3 rayVec, Vector3 rayPos,RaycastHit hit)
    {        
        beamHit = true;
        return new LastHit(this.gameObject.gameObject);
    }
    


    public void NoHit()
    {
         beamHit = false;          
    } 

    
    public bool GetBeamHit() { return beamHit; }
}
