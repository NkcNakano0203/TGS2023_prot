using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FreeRotation : MonoBehaviour, IRotatable
{
    [SerializeField]
    PlayerInput playerInput;

    [SerializeField]
    float theta;
    public void LeftRotate(bool isLeftRotate,bool isRightRotate)
    {
        // ‰ñ“]
        if (isLeftRotate)
            transform.Rotate(0, 0, theta);
    }

    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ‰ñ“]
        if (isRightRotate)
            transform.Rotate(0, 0, -theta);
    }
}
