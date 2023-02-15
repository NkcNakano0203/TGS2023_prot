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
    /// “ü—Í”»’è‚ÍŒã‚ÅÁ‚·
    /// </summary>


    private void Update()
    {
        // ‰E‰ñ“]‚ÌƒL[‚ª‰Ÿ‚³‚ê‚Ä‚¢‚é‚©
        isLeftRotate = leftAction.IsPressed();
        // ¶‰ñ“]‚ÌƒL[‚ª‰Ÿ‚³‚ê‚Ä‚¢‚é‚©
        isRightRotate = rightAction.IsPressed();

        // ‰E‰ñ“]
        LeftRotate(isLeftRotate,isRightRotate);
        // ¶‰ñ“]
        RightRotate(isLeftRotate, isRightRotate);
    }
    public void LeftRotate(bool isLeftRotate,bool isRightRotate)
    {
        // ‰ñ“]
        if (isLeftRotate)
            transform.Rotate(0, 0, -theta);
    }

    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ‰ñ“]
        if (isRightRotate)
            transform.Rotate(0, 0, theta);
    }
}
