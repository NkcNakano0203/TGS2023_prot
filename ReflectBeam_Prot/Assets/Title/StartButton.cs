using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour,IButtonSelect
{
    [SerializeField]
    RestartCounter restartCounter;

    public void Select()
    {
        restartCounter.Reset();
        SceneManager.LoadScene("Stage1");
    }

    public void OnClick()
    {
        Select();
    }
}
