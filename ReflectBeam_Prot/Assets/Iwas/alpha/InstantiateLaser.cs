using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLaser : MonoBehaviour
{
     [SerializeField, Range(0, 1)]
    float scaleOffset = 0.15f;

    // �������郌�[�U�[
    [SerializeField]
    GameObject laserObj;

    // ���������I�u�W�F�N�g
    GameObject instantObj;

    public void LaserTransform(Vector3 startPos, Vector3 hitPos,GameObject laser)
    {
        // ���S���W
        Vector3 vec = hitPos - startPos;
        Vector3 centerPos = Vector3.Lerp(startPos, hitPos, 0.5f);

        // �p�x
        Vector3 vecNor = vec.normalized;
        float radian = Mathf.Atan2(vecNor.x, vecNor.y);

        // �傫��
        float scale = vec.magnitude;

        // ���W�w��
        laser.transform.position = centerPos;
        // �p�x�w��
        laser.transform.rotation = Quaternion.Euler(0, 0, -radian * Mathf.Rad2Deg);
        // �傫���w��
        laser.transform.localScale = new Vector3(scaleOffset, scale, scaleOffset);
    }

    public GameObject AddList_LaserObjs()
    {
        // �������Ď����̃I�u�W�F�N�g�̎q�ɂ���
        var parent = transform;
        instantObj = Instantiate(laserObj, transform.position, Quaternion.identity, parent);
        return instantObj;
    }

    public void RemoveList_LaserObjs(int destroyNum = 0)
    {
        // �����̃I�u�W�F�N�g�̂P�Ԗڂ̎q���폜����
        GameObject childObj = transform.GetChild(destroyNum).gameObject;
        Destroy(childObj);
    }
}
