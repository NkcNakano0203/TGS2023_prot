using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    [SerializeField]
    RestartCounter restartCounter;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }
    public void Click()
    {
        restartCounter.Reset();
        SceneManager.LoadScene("Stage_2");
    }
}
