using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, ISelectable, IRayRecevier
{
    [SerializeField]
    BeamRaycast beamRaycast;
    [SerializeField]
    BeamShot beamShot;

    public GameObject obj;

    public LastHit Hit(Vector3 rayVec, Vector3 rayPos,RaycastHit hit)
    {
        bool isDrow = true;
        LastHit s = new LastHit();

        Vector3 nor_RayVec = rayVec.normalized;
        Vector3 nor_inNor = transform.up;

        Vector3 inNormal = transform.up;

        float dot = Vector3.Dot(nor_RayVec, nor_inNor);

        if ((dot >= 0 && dot <= 0.1f) || (dot <= 0 && dot >= -0.1f))
        {
            Debug.Log("waaaaaaaaaaaaa");
            return s;
        }

        Vector3 normal = (dot < 0) ? -inNormal : inNormal;

        Debug.DrawRay(transform.position, normal, Color.blue);

        // ベクトルを反射させる
        Vector3 reflectVec = Vector3.Reflect(rayVec, normal);

        // レイを再度飛ばす
        //beamRaycast.Ray(reflectVec, rayPos);

        if (hit.collider.gameObject != this)
        {
            isDrow = false;
        }
        
        beamShot.RayShot(rayPos, reflectVec,isDrow);

        return s;
    }

    public SelectAreaInfo GetScale()
    {
        throw new System.NotImplementedException();
    }
}
