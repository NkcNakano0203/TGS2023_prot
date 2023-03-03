using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UniRx;

public class PlayerMove : MonoBehaviour
{
    
    // playerスピード
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

    //地面に向けてレイを飛ばす
    public LayerMask groundLayers;//地面だと認識するレイヤー
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

        // 子オブジェクトを取得
        GameObject playerModel = transform.GetChild(0).gameObject;

        // 子オブジェクトについているAnimatorを取得
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
            // プレイヤー移動
            rb.velocity = new Vector3(player_velocity.x * speed, rb.velocity.y, 0);
            playerAnimator.SetInteger("Action",(int)Action.RUN);
            playerAnimator.SetBool("Jump", false);
        }

        // groundLayersに当たっていたらtrueに
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

    // 移動
    public void OnMove(InputAction.CallbackContext context)
    {
        isMove = true;

        // Move以外は処理しない
        if (context.action.name != "Move")
            return;

        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 移動速度を保持
        player_velocity = new Vector3(axis.x, 0, 0);

        // プレイヤーの向き
        if (axis.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if (axis.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }

    }

    // ジャンプ
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
