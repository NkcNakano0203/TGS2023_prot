using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackUI : MonoBehaviour,IButton
{
    [SerializeField]
    PauseMenuManager GameUIManager;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }
    public void Click()
    {
        GameUIManager.ToggleActive();
    }
}
