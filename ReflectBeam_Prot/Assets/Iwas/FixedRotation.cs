using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FixedRotation : MonoBehaviour, IRotatable
{
    // ‰ñ“]’†‚©‚Ì•Ï”
    bool isRotate = false;

    // ‰ñ“]‚·‚éŠp“x
    readonly float theta = 45f;

    // ‰ñ“]‚·‚éŽžŠÔ
    [SerializeField]
    float rotateTime;

  
    // ‰E‰ñ“]iƒCƒ“ƒ^[ƒtƒF[ƒXj
    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ‰ñ“]’†‚È‚ç‘ŠúƒŠƒ^[ƒ“
        if (isRotate)
            return;

        // THETA•ª‰ñ“]‚·‚é
        transform.DOLocalRotate(new Vector3(0, 0, -theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ‰ñ“]’†‚É‚·‚é
        isRotate = true;
    }

    public void LeftRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ‰ñ“]’†‚È‚ç‘ŠúƒŠƒ^[ƒ“
        if (isRotate)
            return;

        // rotateTime‚©‚¯‚ÄTHETA•ª‰ñ“]‚·‚é
        transform.DOLocalRotate(new Vector3(0, 0, theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ‰ñ“]’†‚É‚·‚é
        isRotate = true;
    }
}
