// 作成日: 作成者:市瀬
using UnityEngine;
using CriWare;

/// <summary>
/// 一番近いレーザーに対する処理
/// </summary>
public class NearLaser : MonoBehaviour, IMinLaserToDistance
{
   private  CriAtomSource criAtomSource;
    // 前の一番近いレーザー
    private GameObject lastNearLaserObject;

    /// <summary>
    /// 最小距離のレーザー
    /// </summary>
    /// <param name="minDistance">最小距離</param>
    /// <param name="laserObject">一番近いレーザーオブジェクト</param>
    public void MinDistanceTolaser(float minDistance, GameObject laserObject)
    {
        Debug.Log(laserObject + " + " + minDistance);
        if (!lastNearLaserObject)
        {
            lastNearLaserObject = laserObject;
            // 一番近いレーザーのソースオン
            criAtomSource = laserObject.GetComponent<CriAtomSource>();
            criAtomSource.enabled = true;
        }
        Debug.Log(lastNearLaserObject.name);


        // 前のレーザーと一番近いレーザーの名前が違うとき
        if (lastNearLaserObject.name != laserObject.name)
        {

            // 前のレーザーのソースオフ
            if (lastNearLaserObject.TryGetComponent(out CriAtomSource atomSource))
            {
                lastNearLaserObject.GetComponent<CriAtomSource>().enabled = false;
            }
            else
            {
                Debug.Log("無理");
            }
            // 一番近いレーザーのソースオン
            criAtomSource = laserObject.GetComponent<CriAtomSource>();
            criAtomSource.enabled = true;
            // 前のレーザーを一番近いレーザーにする
            lastNearLaserObject = laserObject;
        }

        if (minDistance >= 40f)
        {
            criAtomSource.volume = 0.2f;
        }
        else
        {
            float num = 1 - minDistance / 40;
            num += 0.2f;
            criAtomSource.volume = num;
        }
    }
}