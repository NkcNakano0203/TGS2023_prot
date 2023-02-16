using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{

    [SerializeField]
    BeamDrow beamDrow;

    Ray ray;
    RaycastHit hit;

    //レイを返す
    public Ray GetRay() { return ray; }
    //当たった座標を変換
    public RaycastHit GetHit() { return hit; }


    // レイを出す初期位置,レイを飛ばす方向ベクトル
    //レイを飛ばす処理(これを打ち出す処理が呼び出す)
    public void RayShot(Vector3 origin, Vector3 direction,bool isDrow)
    {
        //raylengthをかけた時オーバーフローさせないため
        direction = direction.normalized;


        Physics.Raycast(origin, direction, out hit);

        //Rayが当たった時の処理        
        RayHit(hit, direction);


        beamDrow.DrowShot(hit.point,origin,isDrow);
    }

    // 当たったポイント,レイの方向ベクトル
    void RayHit(RaycastHit hit, Vector3 direction)
    {
        //当たってないときリターン
        if (hit.collider == null) { return; }

        //プレイヤーに当たった時
        if (hit.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }

        //反射物に当たった時
        if (hit.collider.gameObject.TryGetComponent(out IRayRecevier irayRecevier))
        {
            irayRecevier.Hit(direction, hit.point, hit);            
        }
    }
}
