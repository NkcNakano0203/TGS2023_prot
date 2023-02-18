// �쐬��02/17�����j�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// RB��LB�ŃM�~�b�N�I��������X�N���v�g
/// </summary>
[RequireComponent(typeof(GimmickList))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(RightStick_GimmickSelection))]
public class RB_LB_GimmickSelect : MonoBehaviour
{
    // �M�~�b�N��ۊǂ���z��
    private GameObject[] gimmicks;
    private GimmickList gimmickList;
    // ���ݑI�𒆂̃M�~�b�N�̔ԍ�
    private int currentSelectObjectNumber;
    // �ő�I�u�W�F�N�g��
    private int maxObjectNumber;

    private bool is_R_Trigger_Pressed;
    private bool is_L_Trigger_Pressed;
    private InputAction leftAction;
    private InputAction rightAction;
    private FreeRotation freeRotation;
    private Camera mainCamera;
    private Transform aimImageTransform;
    private RectTransform aimRect;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();
        // �M�~�b�N�擾
        gimmicks = gimmickList.gimmickLists;
        maxObjectNumber = gimmicks.Length;
        // �C�x���g�o�^
        RightStick_GimmickSelection rightStick_GimmickSelection = GetComponent<RightStick_GimmickSelection>();
        rightStick_GimmickSelection.CurrentObjectNumber += CurrentObjectNumber;
        // �f���Q�[�g�o�^
        playerInput.actions["L_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["R_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["R_Trigger"].started += On_R_TriggerButton;
        playerInput.actions["L_Trigger"].started += On_L_TriggerButton;

        // ����transform���擾
        aimImageTransform = transform.Find("aimImageCanvas/aimImage").gameObject.transform;
        aimRect = aimImageTransform.parent.GetComponent<RectTransform>();
        mainCamera = Camera.main;
        AimImageMove();

        rightAction = playerInput.actions["R_Trigger"];
        leftAction = playerInput.actions["L_Trigger"];
    }

    private void Update()
    {
        GimmickFreeRotation();
    }

    /// <summary>
    /// ���R��]���̏���
    /// </summary>
    private void GimmickFreeRotation()
    {
        is_R_Trigger_Pressed = rightAction.IsPressed();
        is_L_Trigger_Pressed = leftAction.IsPressed();

        // R��L�̃g���K�[��������Ă���Ƃ���]������
        if (is_R_Trigger_Pressed || is_L_Trigger_Pressed)
        {
            freeRotation = gimmicks[currentSelectObjectNumber].transform.GetChild(0).gameObject.GetComponent<FreeRotation>();
            if (freeRotation)
            {
                freeRotation.RightRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
                freeRotation.LeftRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
            }
        }
    }

    /// <summary>
    /// R1�{�^��
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        if (maxObjectNumber - 1 > currentSelectObjectNumber) { currentSelectObjectNumber++; }
        else { currentSelectObjectNumber = 0; }
        AimImageMove();
    }

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        if (currentSelectObjectNumber == 0) { currentSelectObjectNumber += maxObjectNumber - 1; }
        else { currentSelectObjectNumber--; }
        AimImageMove();
    }

    /// <summary>
    /// R2�{�^��
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        if (gimmicks[currentSelectObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.RightRotate(true, true);
        }
    }

    /// <summary>
    /// L2�{�^��
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        if (gimmicks[currentSelectObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.LeftRotate(true, true);
        }
    }

    /// <summary>
    /// �Ə��摜�̈ړ�����
    /// </summary>
    private void AimImageMove()
    {
        Vector3 targetWorldPos = gimmicks[currentSelectObjectNumber].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImageTransform.localPosition = uiLocalPos;
    }

    /// <summary>
    /// �E�X�e�B�b�N�őI�𒆃I�u�W�F�N�g��ς����Ƃ����Ԗڂɂ�������ʒm���郁�\�b�h
    /// </summary>
    private void CurrentObjectNumber(int number)
    {
        currentSelectObjectNumber = number;
    }
}