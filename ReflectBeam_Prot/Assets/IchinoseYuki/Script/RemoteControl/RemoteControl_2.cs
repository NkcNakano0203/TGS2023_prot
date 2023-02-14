// �쐬��02/14�� �����:�s��
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// �����R���X�N���v�g_2
/// </summary>
public class RemoteControl_2 : MonoBehaviour
{
    [SerializeField, Header("���t���N�^�[��r�[���ȂǑS�ẴM�~�b�N��ۊǂ��郊�X�g")]
    private List<GameObject> gimicks;

    private PlayerInput playerInput;

    // �M�~�b�N�̏���
    private int gimickNumber;
    // �ő�M�~�b�N��
    private int maxGimicks;

    [SerializeField]
    private Material red;
    
    [SerializeField]
    private Material white;

    // ���I�𒆂̃M�~�b�N
    private GameObject nowSelectGimick;
    // ���u���ϐ�
    private GameObject temporaryGimick;
    /// <summary>
    /// t�Ƃ̋���
    /// </summary>
    private float tDistance;
    // 
    [SerializeField]
    List<GameObject> rightSideGimicks;
    // 
    [SerializeField]
    List<GameObject> leftSideGimicks;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // �f���Q�[�g�o�^
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].performed += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].performed += On_L_TriggerButton;

        gimickNumber = 0;
        maxGimicks = gimicks.Count - 1;
        gimicks[gimickNumber].GetComponent<MeshRenderer>().material = red;
        nowSelectGimick = gimicks[gimickNumber].gameObject;
    }

    /// <summary>
    /// R1�{�^��
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        // �I�𒆂̃M�~�b�N�̐F�𔒂ɂ���
        nowSelectGimick.GetComponent<MeshRenderer>().material = white;

        // �I������Ă�����̂��E���̃M�~�b�N��T���o�����\�b�h�����s
        RightSide();

        // ���̏����l
        float temporaryDistance = 1000;
        foreach (GameObject t in rightSideGimicks)
        {
            // ����������
            //Debug.Log(Vector3.Distance(nowSelectGimick.transform.position, t.transform.position), this.gameObject);

            // �Ώۂ̃M�~�b�N�Ƃ̋������v�Z
            tDistance = Vector3.SqrMagnitude(nowSelectGimick.transform.position - t.transform.position);

            // �������l��苗�����߂��Ƃ�
            if(temporaryDistance > tDistance)
            {
                // �������l���㏑��
                temporaryDistance = tDistance;
                // ���u���M�~�b�N��t�M�~�b�N�ŏ㏑��
                temporaryGimick = t;
            }
        }

        // �I�𒆂̃M�~�b�N�����u���M�~�b�N�ŏ㏑��
        nowSelectGimick = temporaryGimick;

        // �I�𒆂̃M�~�b�N�̐F��Ԃɂ���
        nowSelectGimick.GetComponent<MeshRenderer>().material = red;
        Debug.Log(nowSelectGimick.name);
    }

    /// <summary>
    /// �I������Ă�����̂��E���̃M�~�b�N��T���o�����\�b�h
    /// </summary>
    private void RightSide()
    {
        // ���X�g�̏�����
        rightSideGimicks = new List<GameObject>();

        // ���I������Ă���M�~�b�N���X�����傫���M�~�b�N�͐V����List�ɒǉ�
        foreach (GameObject t in gimicks)
        {
            if(nowSelectGimick.transform.position.x < t.transform.position.x)
            {
                rightSideGimicks.Add(t);
            }
        }
    } 

    /// <summary>
    /// L1�{�^��
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        // �I�𒆂̃M�~�b�N�̐F�𔒂ɂ���
        nowSelectGimick.GetComponent<MeshRenderer>().material = white;

        // �I������Ă�����̂�荶���̃M�~�b�N��T���o�����\�b�h�����s
        LeftSide();

        // ���̏����l
        float temporaryDistance = 1000;
        foreach (GameObject t in leftSideGimicks)
        {
            // ����������
            //Debug.Log(Vector3.Distance(nowSelectGimick.transform.position, t.transform.position), this.gameObject);

            // �Ώۂ̃M�~�b�N�Ƃ̋������v�Z
            tDistance = Vector3.SqrMagnitude(nowSelectGimick.transform.position - t.transform.position);

            // �������l��苗�����߂��Ƃ�
            if (temporaryDistance > tDistance)
            {
                // �������l���㏑��
                temporaryDistance = tDistance;
                // ���u���M�~�b�N��t�M�~�b�N�ŏ㏑��
                temporaryGimick = t;
            }
        }

        // �I�𒆂̃M�~�b�N�����u���M�~�b�N�ŏ㏑��
        nowSelectGimick = temporaryGimick;

        // �I�𒆂̃M�~�b�N�̐F��Ԃɂ���
        nowSelectGimick.GetComponent<MeshRenderer>().material = red;
        Debug.Log(nowSelectGimick.name);
    }

    /// <summary>
    /// �I������Ă�����̂�荶���̃M�~�b�N��T���o�����\�b�h
    /// </summary>
    private void LeftSide()
    {
        // ���X�g�̏�����
        leftSideGimicks = new List<GameObject>();

        // ���I������Ă���M�~�b�N���X�����������M�~�b�N�͐V����List�ɒǉ�
        foreach (GameObject t in gimicks)
        {
            if (nowSelectGimick.transform.position.x > t.transform.position.x)
            {
                leftSideGimicks.Add(t);
            }
        }
    }

    /// <summary>
    /// R2�{�^��
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
    }

    /// <summary>
    /// L2�{�^��
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
    }
}