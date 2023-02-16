using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDrow : MonoBehaviour
{
    LineRenderer lineRenderer;
    void Start()
    {
        // 親オブジェクトの持つBeamShotを取得
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 startPos, Vector3 endPos, bool isDrow)
    {
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

        Vector3 _startPos = startPos;
        Vector3 _endPos = endPos; 


        if (!isDrow)
        {
            _startPos = Vector3.zero;
            _endPos = Vector3.zero;
        }

        lineRenderer.SetPosition(0, _startPos);
        lineRenderer.SetPosition(1, _endPos);
    }

}
