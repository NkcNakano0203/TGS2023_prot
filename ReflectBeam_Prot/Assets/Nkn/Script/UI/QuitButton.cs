using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour, IButton
{
    [SerializeField]
    GameUIManager GameUIManager;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }
    public void Click()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
