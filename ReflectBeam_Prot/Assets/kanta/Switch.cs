using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour,IRayRecevier
{
    
    bool beamHit = false;
    public LastHit Hit(Vector3 rayVec, Vector3 rayPos)
    {
        throw new System.NotImplementedException();
    }
    


    public bool BeamHit()
    {        
         return this.beamHit; 
    }


}
