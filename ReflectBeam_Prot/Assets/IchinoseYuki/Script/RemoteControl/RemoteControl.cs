// 作成日02/13日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// リモコンスクリプト
/// </summary>
public class RemoteControl : MonoBehaviour
{
    private PlayerInput playerInput;

    private TestManager testManager;

    // オブジェクトの順番
    private int objectNumber;
    // 最大順番数
    private int maxObjectNumber;

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material white;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // デリゲート登録
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].performed += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].performed += On_L_TriggerButton;
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("R1ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (maxObjectNumber - 1 > objectNumber)
        {
            objectNumber++;
        }
        else
        {
            objectNumber = 0;
        }

        Debug.Log("最大数" + maxObjectNumber + " " + "現在の数" + objectNumber);
        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        Debug.Log("L1ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (objectNumber == 0)
        {
            objectNumber += maxObjectNumber - 1;
        }
        else
        {
            objectNumber--;
        }

        Debug.Log("最大数" + maxObjectNumber + " " + "現在の数" + objectNumber);
        testManager.refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("R2ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    /// <summary>
    /// L2ボタン
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        Debug.Log("L2ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();
    }

    public void CheckObjectName()
    {
        GameObject t = GameObject.FindWithTag("TestManager");

        if (t == null)
        {
            Debug.Log("オブジェクトがないよ");
            return;
        }

        testManager = t.GetComponent<TestManager>();

        if (testManager == null)
        {
            Debug.Log("スクリプトがないよ");
            return;
        }

        for (int i = 0; i < testManager.refrecters.Length; ++i)
        {
            Debug.Log(testManager.refrecters[i].name);
        }

        // objectNumber初期化
        objectNumber = 0;
        // 最大順番数を代入
        maxObjectNumber = testManager.refrecters.Length;
    }
}