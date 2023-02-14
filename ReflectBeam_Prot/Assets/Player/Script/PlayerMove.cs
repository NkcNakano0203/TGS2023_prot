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
    float speed = 3f; 

    bool isJump = false;

    [SerializeField] Ease ease;

    [SerializeField] private Vector3 localGravity;
    Rigidbody rb;

    //地面に向けてレイを飛ばす
    public LayerMask groundLayers;//地面だと認識するレイヤー
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

        // プレイヤー移動
        rb.velocity = new Vector3(player_velocity.x * speed, 0, 0);

        // groundLayersに当たっていたらtrueに
        ishit = Physics.CheckBox(transform.position,Vector3.one * 0.5f,Quaternion.identity,groundLayers);

        if (ishit)
            isJump = false;
        else
            isJump = true;


        // 重力を加えるメソッドを呼ぶ
        if (isJump)
                SetLocalGravity();
        
    }

    // 移動
    public void OnMove(InputAction.CallbackContext context)
    {

        // Move以外は処理しない
        if (context.action.name != "Move")
            return;

        // MoveActionの入力値を取得
        var axis = context.ReadValue<Vector2>();

        // 移動速度を保持
        player_velocity = new Vector3(axis.x, 0, 0);

    }

    // ジャンプ
    public void OnJump(InputAction.CallbackContext context)
    {

        isJump = true;

        if (context.action.name != "Jump")
            return;

        // ジャンプ実行
        if(ishit)
            this.transform.DOMoveY(5f, 1f).SetEase(ease).SetRelative(true);
    }

    public void SetLocalGravity()
    {
        //　重力を加える
        rb.AddForce(localGravity, ForceMode.Acceleration);

    }


    public void PlayerDeath()
    {
        death.Value = true;
    }
}
