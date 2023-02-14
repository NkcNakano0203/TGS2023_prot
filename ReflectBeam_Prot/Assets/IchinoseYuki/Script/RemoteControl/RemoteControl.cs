// �쐬��02/13�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �����R���X�N���v�g
/// </summary>
public class RemoteControl : MonoBehaviour
{
    private PlayerInput playerInput;

    private TestManager testManager;

    // �I�u�W�F�N�g�̏���
    private int objectNumber;
    // �ő叇�Ԑ�
    private int maxObjectNumber;

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material white;

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

        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (maxObjectNumber - 1 > objectNumber)
        {
            objectNumber++;
        }
        else
        {
            objectNumber = 0;
        }

        Debug.Log("�ő吔" + maxObjectNumber + " " + "���݂̐�" + objectNumber);
        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("L1�{�^��" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (objectNumber == 0)
        {
            objectNumber += maxObjectNumber - 1;
        }
        else
        {
            objectNumber--;
        }

        Debug.Log("�ő吔" + maxObjectNumber + " " + "���݂̐�" + objectNumber);
        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
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

    public void CheckObjectName()
    {
        GameObject t = GameObject.FindWithTag("TestManager");

        if (t == null)
        {
            Debug.Log("�I�u�W�F�N�g���Ȃ���");
            return;
        }

        testManager = t.GetComponent<TestManager>();

        if (testManager == null)
        {
            Debug.Log("�X�N���v�g���Ȃ���");
            return;
        }

        for (int i = 0; i < testManager.refrecters.Length; ++i)
        {
            Debug.Log(testManager.refrecters[i].name);
        }

        // objectNumber������
        objectNumber = 0;
        // �ő叇�Ԑ�����
        maxObjectNumber = testManager.refrecters.Length;
    }
}