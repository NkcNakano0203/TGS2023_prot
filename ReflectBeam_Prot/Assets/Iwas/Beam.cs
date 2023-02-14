using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    BeamRaycast beamRaycast;

    // Start is called before the first frame update
    void Start()
    {
        beamRaycast = GetComponent<BeamRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        beamRaycast.Ray(transform.forward,transform.position);
    }
}
