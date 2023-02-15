using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pause;
using UniRx;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject UIPanel;
    [SerializeField]
    PlayerInput playerInput;

    private void Start()
    {
        playerInput.actions["Pause"].performed += OnTogglePause;
    }

    void OnTogglePause(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;

        PauseManager.pause.Value = !PauseManager.pause.Value;
    }

    private void Update()
    {
        PauseManager.pause.Subscribe(x => UIPanel.SetActive(x));
    }
}