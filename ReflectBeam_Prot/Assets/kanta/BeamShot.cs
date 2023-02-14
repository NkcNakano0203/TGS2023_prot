using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    float raylength = float.MaxValue;    

    Ray ray;
    RaycastHit hit;   

    //レイを返す
    public Ray GetRay() { return ray; }
    //当たった座標を変換
    public RaycastHit GetHit() { return hit; }


    //レイを飛ばす処理
    public void RayShot(Vector3 origin, Vector3 direction)
    {       
        Physics.Raycast(origin,direction*raylength, out hit );
        Debug.Log(Physics.Raycast(origin, direction * raylength, out hit));
        Debug.DrawRay(origin, direction * raylength, Color.red);

        RayHit();


    }   
    void RayHit()
    {    
        //プレイヤーに当たった時
        if (hit.collider.gameObject.TryGetComponent<PlayerMove>(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }
    }
}
