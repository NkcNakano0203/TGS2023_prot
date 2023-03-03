using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLaser : MonoBehaviour
{
     [SerializeField, Range(0, 1)]
    float scaleOffset = 0.15f;

    // 生成するレーザー
    [SerializeField]
    GameObject laserObj;

    // 生成したオブジェクト
    GameObject instantObj;

    public void LaserTransform(Vector3 startPos, Vector3 hitPos,GameObject laser)
    {
        // 中心座標
        Vector3 vec = hitPos - startPos;
        Vector3 centerPos = Vector3.Lerp(startPos, hitPos, 0.5f);

        // 角度
        Vector3 vecNor = vec.normalized;
        float radian = Mathf.Atan2(vecNor.x, vecNor.y);

        // 大きさ
        float scale = vec.magnitude;

        // 座標指定
        laser.transform.position = centerPos;
        // 角度指定
        laser.transform.rotation = Quaternion.Euler(0, 0, -radian * Mathf.Rad2Deg);
        // 大きさ指定
        laser.transform.localScale = new Vector3(scaleOffset, scale, scaleOffset);
    }

    public GameObject AddList_LaserObjs()
    {
        // 生成して自分のオブジェクトの子にする
        var parent = transform;
        instantObj = Instantiate(laserObj, transform.position, Quaternion.identity, parent);
        return instantObj;
    }

    public void RemoveList_LaserObjs(int destroyNum = 0)
    {
        // 自分のオブジェクトの１番目の子を削除する
        GameObject childObj = transform.GetChild(destroyNum).gameObject;
        Destroy(childObj);
    }
}
