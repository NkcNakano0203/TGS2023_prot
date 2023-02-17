// �쐬��02/17�����j�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using Pause;

/// <summary>
/// RB��LB�ŃM�~�b�N�I��������X�N���v�g
/// </summary>
[RequireComponent(typeof(GimmickList))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(RightStick_GimmickSelection))]
public class RB_LB_GimmickSelect : MonoBehaviour,IObjectNumber
{
    // �M�~�b�N��ۊǂ���z��
    private GameObject[] gimmicks;
    private GimmickList gimmickList;

    // �I�u�W�F�N�g�̏���
    private int objectNumber;
    // �ő叇�Ԑ�
    private int maxObjectNumber;

    private InputAction leftAction;
    private InputAction rightAction;
    private bool is_R_Trigger_Pressed;
    private bool is_L_Trigger_Pressed;
    private FreeRotation freeRotation;
    private FixedRotation fixedRotation;

    bool isPause = PauseManager.pause.Value;

    // ���C���J����
    private Camera mainCamera;
    // �G�C���摜����ꂽ�I�u�W�F�N�g
    private GameObject aimImage;
    private RectTransform aimRect;
    private PlayerInput playerInput;

    private void Start()
    {
        // ���I�u�W�F�N�g���擾
        aimImage = transform.Find("aimUICanvas/aimImage").gameObject;
        aimRect = aimImage.transform.parent.GetComponent<RectTransform>();
        mainCamera = Camera.main;

        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();

        // �M�~�b�N�擾
        gimmicks = gimmickList.gimmickLists;

        // �f���Q�[�g�o�^
        playerInput.actions["R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["R_Trigger"].started += On_R_TriggerButton;
        playerInput.actions["L_Trigger"].started += On_L_TriggerButton;

        // objectNumber������
        objectNumber = 0;

        // �ő叇�Ԑ�����
        maxObjectNumber = gimmicks.Length;
        // �Ə��ړ�
        Vector3 targetWorldPos = gimmicks[0].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;

        rightAction = playerInput.actions["R_Trigger"];
        leftAction = playerInput.actions["L_Trigger"];
    }

    private void Update()
    {
        // �|�[�Y���͑������^�[��
        if (isPause == true) { return; }

        is_R_Trigger_Pressed = rightAction.IsPressed();
        is_L_Trigger_Pressed = leftAction.IsPressed();

        // R��L�̃g���K�[��������Ă���Ƃ���]������
        if (is_R_Trigger_Pressed || is_L_Trigger_Pressed)
        {
            freeRotation = gimmicks[objectNumber].GetComponent<FreeRotation>();
            if (freeRotation)
            {
                gimmicks[objectNumber].GetComponent<FreeRotation>().RightRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
                gimmicks[objectNumber].GetComponent<FreeRotation>().LeftRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
            }
        }
    }

    /// <summary>
    /// R1�{�^��
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        // �|�[�Y���͑������^�[��
        if (isPause == true){return;}

        if (maxObjectNumber - 1 > objectNumber)
        {
            objectNumber++;
        }
        else
        {
            objectNumber = 0;
        }
        Debug.Log(objectNumber);
        // �Ə��ړ�
        Vector3 targetWorldPos = gimmicks[objectNumber].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;
        ObjectNumber(objectNumber);
    }

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        // �|�[�Y���͑������^�[��
        if (isPause == true) { return; }

        if (objectNumber == 0)
        {
            objectNumber += maxObjectNumber - 1;
        }
        else
        {
            objectNumber--;
        }
        // �Ə��ړ�
        Vector3 targetWorldPos = gimmicks[objectNumber].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;
        ObjectNumber(objectNumber);
    }

    /// <summary>
    /// R2�{�^��
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        // �|�[�Y���͑������^�[��
        if (isPause == true) { return; }

        fixedRotation = gimmicks[objectNumber].GetComponent<FixedRotation>();
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
        // �|�[�Y���͑������^�[��
        if (isPause == true) { return; }

        //if(gimmicks[objectNumber].TryGetComponent(out FixedRotation fixedRotation))
        //{
        //    fixedRotation.LeftRotate(true, true);
        //}

        fixedRotation = gimmicks[objectNumber].GetComponent<FixedRotation>();
        if (fixedRotation)
        {
            fixedRotation.LeftRotate(true, true);
        }
    }

    public int ObjectNumber(int number)
    {
        objectNumber = number;
        return objectNumber;
    }
}