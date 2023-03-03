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
    float speed = 6f;

    [SerializeField]
    float jumpPower = 5f;

    bool isMove = false;
    bool isJump = false;
    bool isJumpAnim = false;

    [SerializeField] Ease ease;

    Rigidbody rb;

    Animator playerAnimator;

    //�n�ʂɌ����ă��C���΂�
    public LayerMask groundLayers;//�n�ʂ��ƔF�����郌�C���[
    bool ishit;

    public IReadOnlyReactiveProperty<bool> DeathProp => death;
    private ReactiveProperty<bool> death = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> GetStar => star;
    private ReactiveProperty<bool> star = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> GetGoal => goal;
    private ReactiveProperty<bool> goal = new ReactiveProperty<bool>(false);


    bool pause;

    enum Action
    {
        Idol,
        RUN,
        Die,
    }

    void Start()
    {
        //PauseManager.pause.Subscribe(x => pause = x);

        rb = this.GetComponent<Rigidbody>();
      

        PlayerInput playerInput = GetComponent<PlayerInput>();

        // �q�I�u�W�F�N�g���擾
        GameObject playerModel = transform.GetChild(0).gameObject;

        // �q�I�u�W�F�N�g�ɂ��Ă���Animator���擾
        playerAnimator = playerModel.GetComponent<Animator>();

        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Jump"].performed += OnJump;

    }

    void Update()
    {
        if (pause) return;

        if (isMove && isJumpAnim == false)
        {
            // �v���C���[�ړ�
            rb.velocity = new Vector3(player_velocity.x * speed, rb.velocity.y, 0);
            playerAnimator.SetInteger("Action",(int)Action.RUN);
            playerAnimator.SetBool("Jump", false);
        }

        // groundLayers�ɓ������Ă�����true��
        ishit = Physics.CheckBox(transform.position,Vector3.one * 0.9f,Quaternion.identity,groundLayers);


        if (ishit)
            isJump = false;

        if (player_velocity == new Vector3(0, 0, 0))
        {
            isMove = false;
            if (isMove == false)
            {
                playerAnimator.SetInteger("Action", (int)Action.Idol);
                playerAnimator.SetBool("Jump", false);
            }
        }

    }

    // �ړ�
    public void OnMove(InputAction.CallbackContext context)
    {
        isMove = true;

        // Move�ȊO�͏������Ȃ�
        if (context.action.name != "Move")
            return;

        // MoveAction�̓��͒l���擾
        var axis = context.ReadValue<Vector2>();

        // �ړ����x��ێ�
        player_velocity = new Vector3(axis.x, 0, 0);

        // �v���C���[�̌���
        if (axis.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (axis.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }

    }

    // �W�����v
    public void OnJump(InputAction.CallbackContext context)
    {

        isJump = true;

        if (context.action.name != "Jump")
            return;

        if (isJump)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            playerAnimator.SetBool("Jump", true);
        }

        isJumpAnim = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GearRotation>(out _))
        {
            other.gameObject.SetActive(false);
            star.Value = true;
        }
        else if(other.TryGetComponent<Goal>(out _))
        {
            goal.Value = true;
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (ishit)
        {
            isJumpAnim = false;
            isJump = false;
        }
    }


    public void PlayerDeath()
    {
        playerAnimator.SetInteger("Action", (int)Action.Die);
        death.Value = true;
    }
}
