using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartUI : MonoBehaviour, IButton
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    public void Click() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}