using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject UIPanel;
    [SerializeField]
    PlayerInput playerInput;

    private void Awake()
    {
        playerInput.actions["Pause"].started += OnTogglePause;
    }
    private void Start()
    {
        UIPanel.gameObject.SetActive(false);
    }

    void OnTogglePause(InputAction.CallbackContext context)
    {
        if (!context.ReadValueAsButton()) return;
        UIPanel.gameObject.SetActive(!UIPanel.gameObject.activeSelf);
    }

    public void ToggleActive()
    {
        UIPanel.gameObject.SetActive(!UIPanel.gameObject.activeSelf);
    }
}