using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStart : MonoBehaviour
{
    [SerializeField]
    BeamDraw beamDraw;

    BeamShot beamShot;
    Vector3 endPos;

    void Start()
    {
        beamShot = GetComponent<BeamShot>();
        beamDraw.AddList(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (endPos != Vector3.zero)
            beamDraw.RemoveList(endPos);
        endPos = beamShot.RayShot(transform.position, transform.up);
        // beamDraw.DrawLine(transform.position, endPos, true);
        
      
        beamDraw.AddList(endPos);
        beamDraw.DrawLine();
    }
}