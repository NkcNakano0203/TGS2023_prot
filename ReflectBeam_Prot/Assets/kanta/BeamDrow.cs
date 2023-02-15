using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDrow : MonoBehaviour
{
    BeamShot beamShot;
    LineRenderer lineRenderer;
    void Start()
    {
        // 親オブジェクトの持つBeamShotを取得
        beamShot = transform.parent.gameObject.GetComponent<BeamShot>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (beamShot.GetHit().point!=Vector3.zero)
        {
            lineRenderer.SetWidth(0.3f, 0.3f);
            lineRenderer.SetPosition(0, beamShot.transform.position);
            lineRenderer.SetPosition(1, beamShot.GetHit().point);
        }
    }
}
