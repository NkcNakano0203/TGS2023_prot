using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot :MonoBehaviour
{
   
    Ray ray;
    RaycastHit hit;   

    //レイを返す
    public Ray GetRay() { return ray; }
    //当たった座標を変換
    public RaycastHit GetHit() { return hit; }


    //レイを飛ばす処理(これを打ち出す処理が呼び出す)
    public void RayShot(Vector3 origin, Vector3 direction)
    {
        //raylengthをかけた時オーバーフローさせないため
        direction = direction.normalized;


        Physics.Raycast(origin,direction, out hit );
       
        //Rayが当たった時の処理        
        RayHit(hit);

    }   

    void RayHit(RaycastHit hit)
    {
        if (hit.collider == null) { return; }
        //プレイヤーに当たった時
        if (hit.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();              
        }
    }
}
