using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FreeRotation : MonoBehaviour, IRotatable
{
    [SerializeField]
    PlayerInput playerInput;

    InputAction rightAction;
    InputAction leftAction;

    bool isRightRotate = false;
    bool isLeftRotate = false;

    [SerializeField]
    float theta;

    private void Start()
    {
        leftAction = playerInput.actions["LeftRotate"];
        rightAction = playerInput.actions["RightRotate"];
    }

    /// <summary>
    /// 入力判定は後で消す
    /// </summary>


    private void Update()
    {
        // 右回転のキーが押されているか
        isLeftRotate = leftAction.IsPressed();
        // 左回転のキーが押されているか
        isRightRotate = rightAction.IsPressed();

        // 右回転
        LeftRotate(isLeftRotate,isRightRotate);
        // 左回転
        RightRotate(isLeftRotate, isRightRotate);
    }
    public void LeftRotate(bool isLeftRotate,bool isRightRotate)
    {
        // 回転
        if (isLeftRotate)
            transform.Rotate(0, 0, -theta);
    }

    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // 回転
        if (isRightRotate)
            transform.Rotate(0, 0, theta);
    }
}
