using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UniRx;

public class PlayerMove : MonoBehaviour
{
    
    // player�X�s�[�h
    private Vector3 player_velocity;
    float speed = 3f; 

    bool isJump = false;

    [SerializeField] Ease ease;

    [SerializeField] private Vector3 localGravity;
    Rigidbody rb;

    //�n�ʂɌ����ă��C���΂�
    public LayerMask groundLayers;//�n�ʂ��ƔF�����郌�C���[
    bool ishit;

    public IReadOnlyReactiveProperty<bool> deathProp => death;
    private ReactiveProperty<bool> death = new ReactiveProperty<bool>(false);


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
      

        PlayerInput playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Jump"].performed += OnJump;

    }

    void Update()
    {

        // �v���C���[�ړ�
        rb.velocity = new Vector3(player_velocity.x * speed, 0, 0);

        // groundLayers�ɓ������Ă�����true��
        ishit = Physics.CheckBox(transform.position,Vector3.one * 0.5f,Quaternion.identity,groundLayers);

        if (ishit)
            isJump = false;
        else
            isJump = true;


        // �d�͂������郁�\�b�h���Ă�
        if (isJump)
                SetLocalGravity();
        
    }

    // �ړ�
    public void OnMove(InputAction.CallbackContext context)
    {

        // Move�ȊO�͏������Ȃ�
        if (context.action.name != "Move")
            return;

        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // �ړ����x��ێ�
        player_velocity = new Vector3(axis.x, 0, 0);

    }

    // �W�����v
    public void OnJump(InputAction.CallbackContext context)
    {

        isJump = true;

        if (context.action.name != "Jump")
            return;

        // �W�����v���s
        if(ishit)
            this.transform.DOMoveY(5f, 1f).SetEase(ease).SetRelative(true);
    }

    public void SetLocalGravity()
    {
        //�@�d�͂�������
        rb.AddForce(localGravity, ForceMode.Acceleration);

    }


    public void PlayerDeath()
    {
        death.Value = true;
    }
}
