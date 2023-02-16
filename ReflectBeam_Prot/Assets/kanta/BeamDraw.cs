using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDraw : MonoBehaviour
{
    LineRenderer lineRenderer;

    [SerializeField]
    float startWidth = 0.15f;
    [SerializeField]
    float endWidth = 0.15f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 drawStartPos, Vector3 drawEndPos, bool isDraw)
    {
        if (!isDraw)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;

        Vector3 startPos = drawStartPos;
        Vector3 endPos = drawEndPos;

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}