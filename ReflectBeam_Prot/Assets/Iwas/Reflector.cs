using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour, ISelectable, IRotatable, IRayRecevier
{
    [SerializeField]
    Vector3 inNormal;

    [SerializeField]
    BeamRaycast beamRaycast;

    public GameObject obj;

    public LastHit Hit(Vector3 rayvec)
    {
        // �x�N�g���𔽎˂�����
        Vector3 reflectVec = Vector3.Reflect(rayvec, inNormal);

        // ���C���ēx��΂�
        beamRaycast.Ray(reflectVec);
        LastHit s = new LastHit();
        return s;
    }

    public SelectAreaInfo GetScale()
    {
        throw new System.NotImplementedException();
    }

    public void Rotate()
    {
        throw new System.NotImplementedException();
    }
}
