using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FreeRotation : MonoBehaviour, IRotatable
{
    [SerializeField]
    float rotateSpeed;
    public void LeftRotate(bool isLeftRotate,bool isRightRotate)
    {
        // ‰ñ“]
        if (isLeftRotate)
            transform.Rotate(0, 0, rotateSpeed);
    }

    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ‰ñ“]
        if (isRightRotate)
            transform.Rotate(0, 0, -rotateSpeed);
    }
}
