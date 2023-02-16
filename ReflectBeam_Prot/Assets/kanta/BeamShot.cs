using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{

    [SerializeField]
    BeamDrow beamDrow;

    bool isDrow;
    Vector3 startPos;

    GameObject hitObj;

    Ray ray;
    RaycastHit hit;

    LastHit lastHit;

    //レイを返す
    public Ray GetRay() { return ray; }
    //当たった座標を変換
    public RaycastHit GetHit() { return hit; }


    private void Update()
    {
        if (hitObj == null)
        {
            Debug.Log("a");
            return;
        }

        if (hitObj != lastHit.lastHitObj)
        {
            if (hitObj.TryGetComponent(out IRayRecevier rayRecevier))
            {
                rayRecevier.NoHit();
            }
        }
    }


    // レイを出す初期位置,レイを飛ばす方向ベクトル
    //レイを飛ばす処理(これを打ち出す処理が呼び出す)
    public Vector3 RayShot(Vector3 origin, Vector3 direction, bool isDrow)
    {
        //raylengthをかけた時オーバーフローさせないため
        direction = direction.normalized;


        Physics.Raycast(origin, direction, out hit);

        //Rayが当たった時の処理        
        RayHit(hit, direction);
        this.isDrow = isDrow;
        startPos = origin;
        lastHit = new LastHit(hit.collider.gameObject);
        return hit.point;
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
            hitObj = hit.collider.gameObject;
            irayRecevier.Hit(direction, hit.point, hit);
        }

    }
}
