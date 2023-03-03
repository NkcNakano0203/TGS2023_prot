using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShot
{
    [SerializeField]
    private InstantiateLaser instantiateLaser;

    // 最後に当たったリフレクターを保存する変数
    GameObject lastHitReflector;

    // 保存する変数
    RayShot rayShot;

    // レーザーの生成の許可
    bool isAddLeser = true;
    // レーザーの削除の許可
    bool isRemoveLaser = false;

    Laser nextLaser;

    public Vector3 RaycastShot(Laser laser)
    {
        Vector3 rayStartPos = laser.GetStartPos();
        Vector3 rayDirection = laser.GetDirection();


        // レイが当たっていなかったらリターン
        if (!Physics.Raycast(rayStartPos, rayDirection, out RaycastHit hit))
            return Vector3.zero;

        // レイの描画
        float maxDistance = (hit.point - rayStartPos).magnitude;
        Debug.DrawRay(rayStartPos, rayDirection * maxDistance, laser.GetColor());

        // 当たったものに IRayReceiver を持っているか
        if (hit.collider.gameObject.TryGetComponent(out IRayReceiverAlpha rayReceiver))
        {
            if (isAddLeser)
            {
                rayShot = new RayShot();

                nextLaser = new Laser(laser.GetColor(), hit.point, rayDirection, rayShot, isAddLeser);
                // IRayReceiver の RayEnter を呼び出す
                rayReceiver.RayEnter(nextLaser);
                isAddLeser = false;
            }
            else
            {
                nextLaser = new Laser(laser.GetColor(), hit.point, rayDirection, rayShot, isAddLeser);
                // IRayReceiver の RayEnter を呼び出す
                rayReceiver.RayEnter(nextLaser);
            }
            isRemoveLaser = true;
            // 最後に当たったリフレクターを取得
            lastHitReflector = hit.collider.gameObject;
        }
        else
        {
            isAddLeser = true;
            if (lastHitReflector != null && isRemoveLaser)
            {
                isRemoveLaser = false;
                lastHitReflector.TryGetComponent(out IRayReceiverAlpha lastHit);

                lastHit.RayExit(nextLaser);
            }
        }

        //プレイヤーに当たった時
        if (hit.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }

        return hit.point;
    }
    public void RayExit()
    {
        // レーザーの生成を許可する
        isAddLeser = true;
        // 初期化
        rayShot = null;
        // 最後に当たったリフレクターがある
        // かつ
        // レーザーを削除する許可がある場合
        if (lastHitReflector != null && isRemoveLaser)
        {
            // レーザーの削除を許可しない
            isRemoveLaser = false;
            // 最後に当たったリフレクターの処理を呼ぶ
            lastHitReflector.TryGetComponent(out IRayReceiverAlpha lastHit);
            lastHit.RayExit(nextLaser);
        }
    }
}