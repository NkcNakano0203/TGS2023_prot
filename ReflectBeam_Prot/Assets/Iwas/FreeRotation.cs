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
    /// ���͔���͌�ŏ���
    /// </summary>


    private void Update()
    {
        // �E��]�̃L�[��������Ă��邩
        isLeftRotate = leftAction.IsPressed();
        // ����]�̃L�[��������Ă��邩
        isRightRotate = rightAction.IsPressed();

        // �E��]
        LeftRotate(isLeftRotate,isRightRotate);
        // ����]
        RightRotate(isLeftRotate, isRightRotate);
    }
    public void LeftRotate(bool isLeftRotate,bool isRightRotate)
    {
        // ��]
        if (isLeftRotate)
            transform.Rotate(0, 0, -theta);
    }

    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ��]
        if (isRightRotate)
            transform.Rotate(0, 0, theta);
    }
}
