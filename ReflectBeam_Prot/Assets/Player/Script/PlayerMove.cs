using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // player�X�s�[�h
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
        // �v���C���[�ړ�
        rb.velocity = new Vector3(player_velocity.x, 0, 0); 
    }
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
}
