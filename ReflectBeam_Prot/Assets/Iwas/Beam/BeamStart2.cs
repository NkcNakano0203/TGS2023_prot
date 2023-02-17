using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStart2 : MonoBehaviour
{
    [SerializeField]
    BeamEffect beamEffect;

    // Update is called once per frame
    void Update()
    {
        beamEffect.Ray(transform.position, transform.up);
    }
}
