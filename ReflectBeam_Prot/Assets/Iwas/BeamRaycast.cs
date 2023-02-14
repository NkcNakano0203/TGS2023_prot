using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRaycast : MonoBehaviour
{

    public void Ray(Vector3 rayvec)
    {
        // レイを飛ばす
        bool rayHit = Physics.Raycast(transform.position, rayvec, out RaycastHit hit);
        // レイを描画
        Debug.DrawRay(transform.position, rayvec, Color.red);

        // レイが当たっていないときはリターン
        if (!rayHit)
            return;

        // レイが当たったゲームオブジェクトがIRayRecevierを持っていたら
        if (!hit.collider.gameObject.TryGetComponent(out IRayRecevier rayRecevier))
            return;

        rayRecevier.Hit(rayvec);

    }
}
