using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject UIPanel;
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    Button firstButton;

    private void Start()
    {
        playerInput.actions["Pause"].started += OnTogglePause;
        UIPanel.gameObject.SetActive(false);
    }

    void OnTogglePause(InputAction.CallbackContext context)
    {
        try
        {
            if (!context.ReadValueAsButton()) return;
            Debug.Log($"UIPanel:{UIPanel.name}", UIPanel);
            Debug.Log($"playerInput:{playerInput.name}");
            ToggleActive();
        }
        catch
        {
            Debug.Log("í‚é~");
        }
    }

    public void ToggleActive()
    {
        UIPanel.gameObject.SetActive(!UIPanel.gameObject.activeSelf);
        if (UIPanel.gameObject.activeSelf)
        {
            eventSystem.firstSelectedGameObject = firstButton.gameObject;
        }
    }
}