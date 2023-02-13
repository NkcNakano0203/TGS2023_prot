using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, IGimmick
{
    public MeshFilter mf;

    public GameObject g;
    private void Start()
    {

    }

    private void Update()
    {
        Vector3 vec = g.transform.position - transform.position;
        Ray ray = new Ray(transform.position, vec);
        Debug.DrawRay(transform.position, vec, Color.black);
        OnHit(vec);
    }

    // �M�~�b�N��UI�T�C�Y
    GimmickUISize IGimmick.GetSize()
    {
        return new GimmickUISize(new Vector2(0, 0), new Vector2(3, 3), Vector2.one);
    }
    // ���������Ƃ�
    public Vector3 OnHit(Vector3 vec)
    {
        Vector3 forward = transform.forward;
        // �O��
        Vector3 result = Vector3.Cross(forward, vec).normalized;
        // ���C������
        Debug.DrawRay(transform.position, result, Color.blue);

        return Vector3.zero;
    }
}
