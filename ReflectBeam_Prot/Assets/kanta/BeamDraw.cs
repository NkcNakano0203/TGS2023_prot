using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDraw : MonoBehaviour
{
    BeamPointList beamPointList = new BeamPointList();

    LineRenderer lineRenderer;

    [SerializeField]
    float startWidth = 0.15f;
    [SerializeField]
    float endWidth = 0.15f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AddList(Vector3 middlePos)
    {
        Debug.Log("’Ç‰Á‚·‚é‚æ");
        beamPointList.AddList(middlePos);
    }
    public void RemoveList(Vector3 middlePos)
    {
        beamPointList.RemoveList(middlePos);
    }


    public void DrawLine()
    {
        lineRenderer.positionCount = beamPointList.GetCountList();
        lineRenderer.SetPositions(beamPointList.GetList());
        foreach (var item in beamPointList.GetList())
        {
            Debug.Log(item);
        }
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