using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, ISelectable, IRayRecevier
{
    [SerializeField]
    BeamShot beamShot;

    [SerializeField]
    BeamDraw beamDraw;

    [SerializeField]
    float protectionAngle = 0.1f;

    System.Action<Vector3> action;

    Vector3 startPos;
    Vector3 endPos;

    public LastHit RayEnter(Vector3 rayVec, Vector3 rayPos)
    {
        LastHit s = new LastHit(gameObject);

        Vector3 nor_RayVec = rayVec.normalized;
        Vector3 nor_inNor = transform.up;

        Vector3 inNormal = transform.up;

        float dot = Vector3.Dot(nor_RayVec, nor_inNor);

        bool isReflectProtection = Mathf.Abs(dot) < protectionAngle;
        if (isReflectProtection)
        {
            RayExit();
            return s;
        }

        Vector3 normal = (dot < 0) ? -inNormal : inNormal;

        Debug.DrawRay(transform.position, normal, Color.blue);

        // ベクトルを反射させる
        Vector3 reflectVec = Vector3.Reflect(rayVec, normal);

        // レイを再度飛ばす
        startPos = rayPos;
        endPos = beamShot.RayShot(rayPos, reflectVec);
        //Debug.Log(endPos);
        //action.Invoke(endPos);
        //beamDraw.DrawLine(startPos, endPos, true);
        beamDraw.AddList(endPos);
        return s;
    }

    public void SetAction(System.Action<Vector3> action)
    {
        this.action = action;
        Debug.Log(action);
    }


    public SelectAreaInfo GetScale()
    {
        throw new System.NotImplementedException();
    }

    public void RayExit()
    {
        Debug.Log("消えるよ");
        // ビームの描写を消す
        // beamDraw.DrawLine(startPos, endPos, false);
       

    }
}
