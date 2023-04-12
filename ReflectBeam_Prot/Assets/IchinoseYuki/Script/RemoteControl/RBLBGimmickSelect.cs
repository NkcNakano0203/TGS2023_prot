// �쐬��02/17�����j�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;

public class RBLBGimmickSelect : MonoBehaviour
{
    // �M�~�b�N��ۊǂ���z��
    private GameObject[] gimmicks;
    private GimmickList gimmickList;
    // ���݂̃M�~�b�N�ԍ�
    private int currentObjectNumber;
    // �ő�I�u�W�F�N�g��
    private int maxObjectNumber;

    private Transform currentObjectTransform;

    private InputAction leftAction;
    private InputAction rightAction;
    private Transform aimImageTransform;
    private PlayerInput playerInput;

    [SerializeField]
    private Material selectMat;
    [SerializeField]
    private Material gimmickMat;
    [SerializeField]
    private string aimImagePath = "aimImageCanvas_2/aimImage_2";

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
        playerInput.actions["L_Shoulder"].performed += OnRightBumper;
        playerInput.actions["R_Shoulder"].performed += OnLeftBumper;
        playerInput.actions["R_Trigger"].started += OnRightTrigger;
        playerInput.actions["L_Trigger"].started += OnLeftTrigger;

        // ����transform���擾
        aimImageTransform = GameObject.Find(aimImagePath).gameObject.transform;
        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        AimImageMove();
        AimImageRotation();

        rightAction = playerInput.actions["R_Trigger"];
        leftAction = playerInput.actions["L_Trigger"];

        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = selectMat;
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
        bool isRightTriggerPressed = rightAction.IsPressed();
        bool isLeftTriggerPressed = leftAction.IsPressed();

        // R��L�̃g���K�[��������Ă���Ƃ���]������
        if (isRightTriggerPressed || isLeftTriggerPressed)
        {
            FreeRotation freeRotation = gimmicks[currentObjectNumber].transform.GetChild(0).gameObject.GetComponent<FreeRotation>();
            if (freeRotation)
            {
                freeRotation.RightRotate(isLeftTriggerPressed, isRightTriggerPressed);
                freeRotation.LeftRotate(isLeftTriggerPressed, isRightTriggerPressed);
                AimImageRotation();
            }
        }
    }

    /// <summary>
    /// R1�{�^��
    /// </summary>
    private void OnRightBumper(InputAction.CallbackContext context)
    {
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = gimmickMat;

        if (maxObjectNumber - 1 > currentObjectNumber) { currentObjectNumber++; }
        else { currentObjectNumber = 0; }

        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = selectMat;
        AimImageMove();
        AimImageRotation();
    }

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void OnLeftBumper(InputAction.CallbackContext context)
    {
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = gimmickMat;

        if (currentObjectNumber == 0) { currentObjectNumber += maxObjectNumber - 1; }
        else { currentObjectNumber--; }

        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = selectMat;
        AimImageMove();
        AimImageRotation();
    }

    /// <summary>
    /// R2�{�^��
    /// </summary>
    private void OnRightTrigger(InputAction.CallbackContext context)
    {
        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        if (gimmicks[currentObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.RightRotate(true, true);
        }
    }

    /// <summary>
    /// L2�{�^��
    /// </summary>
    private void OnLeftTrigger(InputAction.CallbackContext context)
    {
        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        if (gimmicks[currentObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.LeftRotate(true, true);
        }
    }

    /// <summary>
    /// �Ə��摜�̈ړ����\�b�h
    /// </summary>
    private void AimImageMove()
    {
        aimImageTransform.position = currentObjectTransform.position;
    }

    /// <summary>
    /// �Ə��摜�̉�]���\�b�h
    /// </summary>
    private void AimImageRotation()
    {
        aimImageTransform.rotation = currentObjectTransform.rotation;
    }

    /// <summary>
    /// �E�X�e�B�b�N�őI�𒆃I�u�W�F�N�g��ς����Ƃ����Ԗڂɂ�������ʒm���郁�\�b�h
    /// </summary>
    private void CurrentObjectNumber(int number)
    {
        currentObjectNumber = number;
    }

    private void OnDestroy()
    {
        // �������Ă����Ȃ��ƃV�[�����[�h������Missing�G���[���o��
        playerInput.actions["L_Shoulder"].performed -= OnRightBumper;
        playerInput.actions["R_Shoulder"].performed -= OnLeftBumper;
        playerInput.actions["R_Trigger"].started -= OnRightTrigger;
        playerInput.actions["L_Trigger"].started -= OnLeftTrigger;
    }
}