// 作成日02/13日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// リモコンスクリプト
/// </summary>
public class RemoteControl : MonoBehaviour
{
    // リフレクター用配列
    private GameObject[] Reflector;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // デリゲート登録
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].performed += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].performed += On_L_TriggerButton;
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("R1ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("L1ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("R2ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// L2ボタン
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("L2ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }
}