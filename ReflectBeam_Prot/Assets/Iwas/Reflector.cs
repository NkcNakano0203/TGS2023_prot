using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, ISelectable, IRayRecevier
{
    [SerializeField]
    BeamShot beamShot;

    [SerializeField]
    BeamDrow beamDrow;

    Vector3 startPos;
    Vector3 endPos;

    public LastHit Hit(Vector3 rayVec, Vector3 rayPos, RaycastHit hit)
    {
        // いらないかも
        bool isDrow = true;


        LastHit s = new LastHit(gameObject);

        Vector3 nor_RayVec = rayVec.normalized;
        Vector3 nor_inNor = transform.up;

        Vector3 inNormal = transform.up;

        float dot = Vector3.Dot(nor_RayVec, nor_inNor);

        if ((dot >= 0 && dot <= 0.1f) || (dot <= 0 && dot >= -0.1f))
        {           
            return s;
        }

        Vector3 normal = (dot < 0) ? -inNormal : inNormal;

        Debug.DrawRay(transform.position, normal, Color.blue);

        // ベクトルを反射させる
        Vector3 reflectVec = Vector3.Reflect(rayVec, normal);

        // レイを再度飛ばす
        this.startPos = rayPos;
        endPos = beamShot.RayShot(rayPos, reflectVec, isDrow);
        beamDrow.DrowShot(endPos, startPos, true);

        return s;
    }


    public SelectAreaInfo GetScale()
    {
        throw new System.NotImplementedException();
    }

    public void NoHit()
    {
        // ビームの描写を消す
        beamDrow.DrowShot(endPos, startPos, false);

    }
}
