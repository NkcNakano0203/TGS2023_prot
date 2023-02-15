// �쐬��02/13�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;
using Pause;

/// <summary>
/// �����R���X�N���v�g
/// </summary>
public class RemoteControl : MonoBehaviour
{
    private PlayerInput playerInput;

    // �I�u�W�F�N�g�̏���
    private int objectNumber;
    // �ő叇�Ԑ�
    private int maxObjectNumber;

    // ���t���N�^�[�����郊�X�g
    [Header("���t���N�^�[������z��ł��B")]
    public GameObject[] refrecters;

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material white;

    InputAction leftAction;
    InputAction rightAction;
    bool is_R_Trigger_Pressed;
    bool is_L_Trigger_Pressed;
    FreeRotation freeRotation;
    FixedRotation fixedRotation;

    bool isPause = PauseManager.pause.Value;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // �f���Q�[�g�o�^
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].started += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].started += On_L_TriggerButton;

        // objectNumber������
        objectNumber = 0;
        // �ő叇�Ԑ�����
        maxObjectNumber = refrecters.Length;
        refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;

        rightAction = playerInput.actions["RemoteControl_R_Trigger"];
        leftAction = playerInput.actions["RemoteControl_L_Trigger"];
    }

    private void Update()
    {
        if (isPause == true)
        {
            return;
        }

        // ���t���N�^�[�z��̒��g����̎����^�[��
        if(refrecters.Length <= 0)
        {
            return;
        }

        is_R_Trigger_Pressed = rightAction.IsPressed();
        is_L_Trigger_Pressed = leftAction.IsPressed();
        Debug.Log(is_R_Trigger_Pressed + " " + is_L_Trigger_Pressed);

        if (is_R_Trigger_Pressed || is_L_Trigger_Pressed)
        {
            freeRotation = refrecters[objectNumber].GetComponent<FreeRotation>();
            if (freeRotation)
            {
                refrecters[objectNumber].GetComponent<FreeRotation>().RightRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed); ;
                refrecters[objectNumber].GetComponent<FreeRotation>().LeftRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed); ;
            }
        }
    }

    /// <summary>
    /// R1�{�^��
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        if(isPause == true)
        {
            return;
        }

        // ���t���N�^�[�z��̒��g����̎����^�[��
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("R1�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (maxObjectNumber - 1 > objectNumber)
        {
            objectNumber++;
        }
        else
        {
            objectNumber = 0;
        }

        Debug.Log("�ő吔" + maxObjectNumber + " " + "���݂̐�" + objectNumber);
        refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        if (isPause == true)
        {
            return;
        }

        // ���t���N�^�[�z��̒��g����̎����^�[��
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("L1�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (objectNumber == 0)
        {
            objectNumber += maxObjectNumber - 1;
        }
        else
        {
            objectNumber--;
        }

        Debug.Log("�ő吔" + maxObjectNumber + " " + "���݂̐�" + objectNumber);
        refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// R2�{�^��
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        if (isPause == true)
        {
            return;
        }

        // ���t���N�^�[�z��̒��g����̎����^�[��
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("R2�{�^��" + context.ReadValueAsButton());
        fixedRotation = refrecters[objectNumber].GetComponent<FixedRotation>();
        if (fixedRotation)
        {
            fixedRotation.RightRotate(true, true);
        }
    }

    /// <summary>
    /// L2�{�^��
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        if (isPause == true)
        {
            return;
        }

        // ���t���N�^�[�z��̒��g����̎����^�[��
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("L2�{�^��" + context.ReadValueAsButton());
        fixedRotation = refrecters[objectNumber].GetComponent<FixedRotation>();
        if (fixedRotation)
        {
            fixedRotation.LeftRotate(true, true);
        }
    }
}