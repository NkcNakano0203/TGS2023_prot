using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDrow : MonoBehaviour
{
    BeamShot beamShot;
    LineRenderer lineRenderer;
    void Start()
    {
        // �e�I�u�W�F�N�g�̎���BeamShot���擾
        beamShot = transform.parent.gameObject.GetComponent<BeamShot>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrowShot(Vector3 drowEndPos, Vector3 drowStartPos, bool isDrow)
    {
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

        Vector3 startPos = drowStartPos;
        Vector3 endPos = drowEndPos;

       
        if (!isDrow)
        {
            Debug.Log("���Z�b�g");
            startPos = Vector3.zero;
            endPos = Vector3.zero;
        }
       
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
