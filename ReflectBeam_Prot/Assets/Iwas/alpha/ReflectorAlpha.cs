using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorAlpha : MonoBehaviour, IRayReceiverAlpha
{
    [SerializeField]
    InstantiateLaser instantLaser;

    [SerializeField]
    float protectAngle;

    RayShot rayShot;

    List<GameObject> lasers = new List<GameObject>();
   
    int count = 0;

    public void RayEnter(Laser laser)
    {

        Vector3 startPos = laser.GetStartPos();
        Vector3 direction = laser.GetDirection();
        rayShot = laser.GetRayShot();


        // ベクトルを取得
        Vector3 dir = direction;

        // レイのベクトルを正規化
        Vector3 nor_direction = dir.normalized;

        // 法線を取得
        Vector3 normal = transform.up;

        // 法線とレイのベクトルの内積
        float dot = Vector3.Dot(nor_direction, normal);

        // 絶対値dot が反射できない角度より小さかったら false 
        bool isProtect = Mathf.Abs(dot) < protectAngle * Mathf.Deg2Rad;
        if (isProtect)
            return;

        // dot がマイナス値だったら法線を反転させる
        Vector3 inNormal = dot < 0 ? -normal : normal;

        // 受け取ったベクトルを反射させる
        Vector3 reflectVec = Vector3.Reflect(dir, inNormal);

        // 法線を描写する
        Debug.DrawRay(transform.position, inNormal, Color.blue);

        Laser nextLaser = new Laser(laser.GetColor(), startPos, reflectVec, null, true);

        // レイを撃ちだす
        Vector3 hitPos = rayShot.RaycastShot(nextLaser);

        // レーザーを生成してよいか
        if (laser.GetIsInstantLaser())
        {
            // 生成
            GameObject obj = instantLaser.AddList_LaserObjs();
            lasers.Add(obj);
        }

        if (count > lasers.Count - 1)
        {
            count = 0;
        }
        // レーザーのtransformを変更
        instantLaser.LaserTransform(startPos,hitPos, lasers[count]);

        count++;
    
        return;
    }

    public void RayExit(Laser laser)
    {
        // rayShot が null だったらリターン
        if (laser.GetRayShot() == null)
        {
            return;
        }
        // レイが当たっていない処理を呼ぶ
        rayShot.RayExit();
        lasers.RemoveAt(0);
        //lasers.Clear();
        // レーザーを削除する
        instantLaser.RemoveList_LaserObjs();
    }
}