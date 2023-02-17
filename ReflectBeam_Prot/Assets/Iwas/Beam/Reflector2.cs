using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector2 : MonoBehaviour, IRayRecevier2
{
    [SerializeField]
    BeamEffect beamEffect;

    [SerializeField]
    float protectionAngle;

    public void RayEnter(Vector3 startPos, Vector3 rayVec)
    {
        Vector3 nor_rayVec = rayVec.normalized;
        Vector3 normal = transform.up;

        float dot = Vector3.Dot(nor_rayVec, normal);

        bool isProtection = Mathf.Abs(dot) < protectionAngle;
        if(isProtection)
        {
            return;
        }

        Vector3 inNormal = (dot < 0) ? -normal : normal;
        Debug.DrawRay(transform.position, inNormal, Color.blue);

        Vector3 reflectVec = Vector3.Reflect(rayVec, inNormal);

        beamEffect.Ray(startPos, reflectVec);
        return;
    }

    public void RayExit()
    {
        beamEffect.RayExit();
    }
}
