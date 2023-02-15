// 作成日02/13日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using Pause;

/// <summary>
/// リモコンスクリプト
/// </summary>
public class RemoteControl : MonoBehaviour
{
    private PlayerInput playerInput;

    // オブジェクトの順番
    private int objectNumber;
    // 最大順番数
    private int maxObjectNumber;

    // リフレクターを入れるリスト
    [Header("リフレクターを入れる配列です。")]
    public GameObject[] refrecters;

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material white;

    InputAction leftAction;
    InputAction rightAction;
    bool is_R_Trigger_Pressed;
    bool is_L_Trigger_Pressed;
    FreeRotation freeRotation;
    FixedRotation fixedRotation;

    bool isPause = PauseManager.pause.Value;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // デリゲート登録
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].started += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].started += On_L_TriggerButton;

        // objectNumber初期化
        objectNumber = 0;
        // 最大順番数を代入
        maxObjectNumber = refrecters.Length;
        refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;

        rightAction = playerInput.actions["RemoteControl_R_Trigger"];
        leftAction = playerInput.actions["RemoteControl_L_Trigger"];
    }

    private void Update()
    {
        if (isPause == true)
        {
            return;
        }

        // リフレクター配列の中身が空の時リターン
        if(refrecters.Length <= 0)
        {
            return;
        }

        is_R_Trigger_Pressed = rightAction.IsPressed();
        is_L_Trigger_Pressed = leftAction.IsPressed();
        Debug.Log(is_R_Trigger_Pressed + " " + is_L_Trigger_Pressed);

        if (is_R_Trigger_Pressed || is_L_Trigger_Pressed)
        {
            freeRotation = refrecters[objectNumber].GetComponent<FreeRotation>();
            if (freeRotation)
            {
                refrecters[objectNumber].GetComponent<FreeRotation>().RightRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed); ;
                refrecters[objectNumber].GetComponent<FreeRotation>().LeftRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed); ;
            }
        }
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        if(isPause == true)
        {
            return;
        }

        // リフレクター配列の中身が空の時リターン
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("R1ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (maxObjectNumber - 1 > objectNumber)
        {
            objectNumber++;
        }
        else
        {
            objectNumber = 0;
        }

        Debug.Log("最大数" + maxObjectNumber + " " + "現在の数" + objectNumber);
        refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        if (isPause == true)
        {
            return;
        }

        // リフレクター配列の中身が空の時リターン
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("L1ボタン" + context.ReadValueAsButton());
        bool isButtonDown = context.ReadValueAsButton();

        refrecters[objectNumber].GetComponent<MeshRenderer>().material = white;

        if (objectNumber == 0)
        {
            objectNumber += maxObjectNumber - 1;
        }
        else
        {
            objectNumber--;
        }

        Debug.Log("最大数" + maxObjectNumber + " " + "現在の数" + objectNumber);
        refrecters[objectNumber].GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        if (isPause == true)
        {
            return;
        }

        // リフレクター配列の中身が空の時リターン
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("R2ボタン" + context.ReadValueAsButton());
        fixedRotation = refrecters[objectNumber].GetComponent<FixedRotation>();
        if (fixedRotation)
        {
            fixedRotation.RightRotate(true, true);
        }
    }

    /// <summary>
    /// L2ボタン
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        if (isPause == true)
        {
            return;
        }

        // リフレクター配列の中身が空の時リターン
        if (refrecters.Length <= 0)
        {
            return;
        }

        Debug.Log("L2ボタン" + context.ReadValueAsButton());
        fixedRotation = refrecters[objectNumber].GetComponent<FixedRotation>();
        if (fixedRotation)
        {
            fixedRotation.LeftRotate(true, true);
        }
    }
}