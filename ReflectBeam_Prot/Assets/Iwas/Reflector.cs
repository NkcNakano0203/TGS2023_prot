using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour,IGimmick
{
    public MeshFilter mf;

    // ギミックのUIサイズ
    GimmickUISize IGimmick.GetSize()
    {
        return new GimmickUISize(new Vector2(0, 0), new Vector2(3, 3), Vector2.one);
    }
    // 当たったとき
    public Vector3 OnHit()
    {
       return Vector3.forward;
    }
}
