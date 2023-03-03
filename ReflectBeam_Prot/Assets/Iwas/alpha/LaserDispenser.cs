using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDispenser : MonoBehaviour
{
    [SerializeField]
    InstantiateLaser instantiateLaser;

    RayShot rayShot;

    GameObject laserObj;
    Laser laser;

    private void Awake()
    {
        rayShot =  new RayShot();

        laserObj = instantiateLaser.AddList_LaserObjs();
    }

    private void Update()
    {

        // レイの発射地点
        Vector3 rayStartPos = transform.position;
        // レイの方向
        Vector3 rayDirection = transform.right;

        laser = new Laser(Color.red, rayStartPos, rayDirection, null, true);

        // レイを撃ちだし、当たった座標をもらう
        Vector3 hitPos = rayShot.RaycastShot(laser);
        // レーザーがまだ描画されていなかったら
  
       // レーザーのtransformの変更
        instantiateLaser.LaserTransform(laser.GetStartPos(), hitPos,laserObj);
    }
}