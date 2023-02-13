// �쐬��02/13�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �����R���X�N���v�g
/// </summary>
public class RemoteControl : MonoBehaviour
{
    // ���t���N�^�[�p�z��
    private GameObject[] Reflector;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // �f���Q�[�g�o�^
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].performed += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].performed += On_L_TriggerButton;
    }

    /// <summary>
    /// R1�{�^��
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("R1�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("L1�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// R2�{�^��
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("R2�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// L2�{�^��
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("L2�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }
}