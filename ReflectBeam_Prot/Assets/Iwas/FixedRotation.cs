using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FixedRotation : MonoBehaviour, IRotatable
{
    [SerializeField]
    PlayerInput playerInput;

    // 回転中かの変数
    bool isRotate = false;

    // 回転する角度
    readonly float theta = 45f;

    // 回転する時間
    [SerializeField]
    float rotateTime;

    private void Start()
    {
        playerInput.actions["LeftRotate"].started += OnLeftRotated;
        playerInput.actions["RightRotate"].started += OnRightRotated;
    }
    /// <summary>
    /// 入力判定は後で消す
    /// </summary>

    // 入力(左回転)
    public void OnLeftRotated(InputAction.CallbackContext context)
    {
        LeftRotate(true, true);
    }

    // 入力(右回転)
    public void OnRightRotated(InputAction.CallbackContext context)
    {
        RightRotate(true, true);
    }


    // 右回転（インターフェース）
    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // 回転中なら早期リターン
        if (isRotate)
            return;

        // THETA分回転する
        transform.DOLocalRotate(new Vector3(0, 0, -theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // 回転中にする
        isRotate = true;
    }

    public void LeftRotate(bool isLeftRotate, bool isRightRotate)
    {
        // 回転中なら早期リターン
        if (isRotate)
            return;

        // rotateTimeかけてTHETA分回転する
        transform.DOLocalRotate(new Vector3(0, 0, theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // 回転中にする
        isRotate = true;
    }
}
