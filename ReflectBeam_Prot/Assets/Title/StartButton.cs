using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour,IButtonSelect
{
    public void Select()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void OnClick()
    {
        Select();
    }
}
