using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FixedRotation : MonoBehaviour, IRotatable
{
    [SerializeField]
    PlayerInput playerInput;

    // ‰ñ“]’†‚©‚Ì•Ï”
    bool isRotate = false;

    // ‰ñ“]‚·‚éŠp“x
    readonly float theta = 45f;

    // ‰ñ“]‚·‚éŠÔ
    [SerializeField]
    float rotateTime;

    private void Start()
    {
        playerInput.actions["LeftRotate"].started += OnLeftRotated;
        playerInput.actions["RightRotate"].started += OnRightRotated;
    }
    /// <summary>
    /// “ü—Í”»’è‚ÍŒã‚ÅÁ‚·
    /// </summary>

    // “ü—Í(¶‰ñ“])
    public void OnLeftRotated(InputAction.CallbackContext context)
    {
        LeftRotate(true, true);
    }

    // “ü—Í(‰E‰ñ“])
    public void OnRightRotated(InputAction.CallbackContext context)
    {
        RightRotate(true, true);
    }


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
