using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pause;

public class BackUI : MonoBehaviour,IButton
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }
    public void Click()
    {
        PauseManager.pause.Value = false;
    }
}
