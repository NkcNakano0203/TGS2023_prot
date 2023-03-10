using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : MonoBehaviour
{
    [SerializeField]
    GameObject rayObj;

    [SerializeField, Range(0, 1)]
    float scaleOffset = 2;

    Vector3 lastHitPoint;

    GameObject lastHitReflector;
    GameObject lastHit;

    bool isRemove = true;
    private void Update()
    {
    }

    public void Ray(Vector3 startPos, Vector3 direction)
    {
        bool rayHit = Physics.Raycast(startPos, direction, out RaycastHit hit);

        Debug.DrawRay(startPos, direction, Color.red);

        Vector3 vec = hit.point - startPos;
        Vector3 centerPos = Vector3.Lerp(startPos, hit.point, 0.5f);

        Vector3 vecNor = vec.normalized;
        float radian = Mathf.Atan2(vecNor.x, vecNor.y);

        float scale = vec.magnitude;

        rayObj.transform.position = centerPos;
        rayObj.transform.rotation = Quaternion.Euler(0, 0, -radian * Mathf.Rad2Deg);
        rayObj.transform.localScale = new Vector3(scaleOffset, scale, scaleOffset);



        if (!rayHit)
        {
            rayObj.SetActive(false);
            return;
        }
        rayObj.SetActive(true);

        if (hit.collider.gameObject.TryGetComponent(out IRayRecevier2 rayRecevier))
        {
            lastHitReflector = hit.collider.gameObject;
            rayRecevier.RayEnter(hit.point, direction);
            isRemove = true;
        }
        else
        {
            if (isRemove&& lastHitReflector != null)
            {
                isRemove = false;
                lastHitReflector.TryGetComponent(out IRayRecevier2 lastHit);
                lastHit.RayExit();

            }
        }
        if (hit.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }
        if (hit.point == lastHitPoint)
            return;
        lastHitPoint = hit.point;
        lastHit = hit.collider.gameObject;
    }

    public void RayExit()
    {
        rayObj.SetActive(false);
        if (lastHitReflector == null)
            return;
        if (isRemove)
        {
            isRemove = false;
            lastHitReflector.TryGetComponent(out IRayRecevier2 rayRecevier);
            rayRecevier.RayExit();
        }
    }

}
