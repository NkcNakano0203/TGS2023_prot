using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    float raylength = float.MaxValue;    

    Ray ray;
    RaycastHit hit;   

    //ƒŒƒC‚ğ•Ô‚·
    public Ray GetRay() { return ray; }
    //“–‚½‚Á‚½À•W‚ğ•ÏŠ·
    public RaycastHit GetHit() { return hit; }


    //ƒŒƒC‚ğ”ò‚Î‚·ˆ—
    public void RayShot(Vector3 origin, Vector3 direction)
    {       
        Physics.Raycast(origin,direction*raylength, out hit );
        Debug.Log(Physics.Raycast(origin, direction * raylength, out hit));
        Debug.DrawRay(origin, direction * raylength, Color.red);
    }   
}
