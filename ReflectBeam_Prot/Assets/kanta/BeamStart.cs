using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStart : MonoBehaviour
{
    [SerializeField]
    BeamDraw beamDraw;

    BeamShot beamShot;

    void Start()
    {
        beamShot = GetComponent<BeamShot>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endPos = beamShot.RayShot(transform.position, transform.up);
        beamDraw.DrawLine(transform.position, endPos, true);
    }
}