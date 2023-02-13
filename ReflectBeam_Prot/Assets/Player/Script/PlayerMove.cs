using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // playerスピード
    private Vector3 player_velocity;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayerInput playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Move"].performed += OnMove;
    }

    void Update()
    {
        // プレイヤー移動
        rb.velocity = new Vector3(player_velocity.x, 0, 0); 
    }
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
}
