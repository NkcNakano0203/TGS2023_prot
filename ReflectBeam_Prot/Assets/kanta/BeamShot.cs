using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    [SerializeField]
    BeamDraw beamDraw;

    GameObject hitObj;
    RaycastHit raycastHit;
    LastHit lastHit;

    private void Update()
    {
        if (hitObj == null)
        {
            return;
        }
        Debug.Log(lastHit.lastHitObj);
        if (hitObj != lastHit.lastHitObj)
        {
            if (hitObj.TryGetComponent(out IRayRecevier rayRecevier))
            {
                rayRecevier.RayExit();
            }
        }
    }


    // レイを出す初期位置,レイを飛ばす方向ベクトル
    //レイを飛ばす処理(これを打ち出す処理が呼び出す)
    public Vector3 RayShot(Vector3 origin, Vector3 direction)
    {
        //raylengthをかけた時オーバーフローさせないため
        direction = direction.normalized;


        RaycastHit[] hits = Physics.RaycastAll(origin, direction);
        Debug.DrawRay(origin, direction, Color.red);


        foreach (var item in hits)
        {
            //Rayが当たった時の処理        
            raycastHit = item;

            // 当たったものが自分だったらcontinue
            if (item.collider.gameObject == gameObject)
                continue;

            //プレイヤーに当たった時
            if (item.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
            {
                playerMove.PlayerDeath();
                break;
            }

            //反射物に当たった時
            if (item.collider.gameObject.TryGetComponent(out IRayRecevier irayRecevier))
            {
                hitObj = item.collider.gameObject;
                irayRecevier.RayEnter(direction, item.point);

               bool hoge = item.collider.gameObject.TryGetComponent(out Reflector reflector);

                //

                //

                
                reflector.SetAction(beamDraw.AddList);

                break;
            }
        }

        lastHit = new LastHit(raycastHit.collider.gameObject);
        return raycastHit.point;
    }
}
