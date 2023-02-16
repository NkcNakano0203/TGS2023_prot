using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, ISelectable, IRayRecevier
{
    [SerializeField]
    BeamShot beamShot;

    [SerializeField]
    BeamDrow beamDrow;

    [SerializeField]
    float protectionAngle;

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

        // �x�N�g���𔽎˂�����
        Vector3 reflectVec = Vector3.Reflect(rayVec, normal);

        // ���C���ēx��΂�
        startPos = rayPos;
        endPos = beamShot.RayShot(rayPos, reflectVec);
        beamDrow.DrawLine(startPos, endPos, true);

        return s;
    }


    public SelectAreaInfo GetScale()
    {
        throw new System.NotImplementedException();
    }

    public void RayExit()
    {
        // �r�[���̕`�ʂ�����
        beamDrow.DrawLine(startPos, endPos, false);

    }
}
