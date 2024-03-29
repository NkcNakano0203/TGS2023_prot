using UnityEngine;
using DG.Tweening;

public class FixedRotation_2 : MonoBehaviour, IRotatable
{
    // 回転中かの変数
    private bool isRotate = false;

    // 回転する角度
    const float theta = 45f;

    // 回転する時間
    [SerializeField]
    private float rotateTime;

    // 右回転（インターフェース）
    public void RightRotate()
    {
        // 回転中なら早期リターン
        if (isRotate) { return; }

        // THETA分回転する
        transform.DOLocalRotate(new Vector3(0, 0, -theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // 回転中にする
        isRotate = true;
    }

    public void LeftRotate()
    {
        // 回転中なら早期リターン
        if (isRotate) { return; }

        // rotateTimeかけてTHETA分回転する
        transform.DOLocalRotate(new Vector3(0, 0, theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // 回転中にする
        isRotate = true;
    }
}