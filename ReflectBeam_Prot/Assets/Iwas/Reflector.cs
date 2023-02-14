using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, ISelectable, IRayRecevier
{
    [SerializeField]
    Vector3 inNormal;

    [SerializeField]
    BeamRaycast beamRaycast;

    public GameObject obj;

    public LastHit Hit(Vector3 rayVec,Vector3 rayPos)
    {
        // ベクトルを反射させる
        Vector3 reflectVec = Vector3.Reflect(rayVec, inNormal);

        // レイを再度飛ばす
        beamRaycast.Ray(reflectVec,rayPos);
        LastHit s = new LastHit();
        return s;
    }

    public SelectAreaInfo GetScale()
    {
        throw new System.NotImplementedException();
    }
}
