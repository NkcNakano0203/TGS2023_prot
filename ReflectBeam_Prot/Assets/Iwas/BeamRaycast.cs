using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRaycast : MonoBehaviour
{
    public void Ray(Vector3 rayvec,Vector3 rayPos)
    {
        // レイを飛ばす
        bool rayHit = Physics.Raycast(rayPos, rayvec, out RaycastHit hit);
        // レイを描画
        Debug.DrawRay(rayPos, rayvec, Color.red);

        // レイが当たっていないときはリターン
        if (!rayHit)
            return;

        // レイが当たったゲームオブジェクトがIRayRecevierを持っていたら
        if (!hit.collider.gameObject.TryGetComponent(out IRayRecevier2 rayRecevier))
            return;
        //Debug.Log(hit.point);

      //  rayRecevier.Hit(rayvec,hit.point);
    }
}
