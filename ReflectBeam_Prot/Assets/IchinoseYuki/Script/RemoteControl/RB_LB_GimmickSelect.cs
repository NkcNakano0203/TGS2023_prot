// 作成日02/17日金曜日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using Pause;

/// <summary>
/// RBとLBでギミック選択をするスクリプト
/// </summary>
[RequireComponent(typeof(GimmickList))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(RightStick_GimmickSelection))]
public class RB_LB_GimmickSelect : MonoBehaviour,IObjectNumber
{
    // ギミックを保管する配列
    private GameObject[] gimmicks;
    private GimmickList gimmickList;

    // オブジェクトの順番
    private int objectNumber;
    // 最大順番数
    private int maxObjectNumber;

    private InputAction leftAction;
    private InputAction rightAction;
    private bool is_R_Trigger_Pressed;
    private bool is_L_Trigger_Pressed;
    private FreeRotation freeRotation;
    private FixedRotation fixedRotation;

    bool isPause = PauseManager.pause.Value;

    // メインカメラ
    private Camera mainCamera;
    // エイム画像を入れたオブジェクト
    private GameObject aimImage;
    private RectTransform aimRect;
    private PlayerInput playerInput;

    private void Start()
    {
        // 孫オブジェクトを取得
        aimImage = transform.Find("aimUICanvas/aimImage").gameObject;
        aimRect = aimImage.transform.parent.GetComponent<RectTransform>();
        mainCamera = Camera.main;

        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();

        // ギミック取得
        gimmicks = gimmickList.gimmickLists;

        // デリゲート登録
        playerInput.actions["R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["R_Trigger"].started += On_R_TriggerButton;
        playerInput.actions["L_Trigger"].started += On_L_TriggerButton;

        // objectNumber初期化
        objectNumber = 0;

        // 最大順番数を代入
        maxObjectNumber = gimmicks.Length;
        // 照準移動
        Vector3 targetWorldPos = gimmicks[0].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;

        rightAction = playerInput.actions["R_Trigger"];
        leftAction = playerInput.actions["L_Trigger"];
    }

    private void Update()
    {
        // ポーズ中は早期リターン
        if (isPause == true) { return; }

        is_R_Trigger_Pressed = rightAction.IsPressed();
        is_L_Trigger_Pressed = leftAction.IsPressed();

        // RかLのトリガーが押されているとき回転させる
        if (is_R_Trigger_Pressed || is_L_Trigger_Pressed)
        {
            freeRotation = gimmicks[objectNumber].GetComponent<FreeRotation>();
            if (freeRotation)
            {
                gimmicks[objectNumber].GetComponent<FreeRotation>().RightRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
                gimmicks[objectNumber].GetComponent<FreeRotation>().LeftRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
            }
        }
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        // ポーズ中は早期リターン
        if (isPause == true){return;}

        if (maxObjectNumber - 1 > objectNumber)
        {
            objectNumber++;
        }
        else
        {
            objectNumber = 0;
        }
        Debug.Log(objectNumber);
        // 照準移動
        Vector3 targetWorldPos = gimmicks[objectNumber].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;
        ObjectNumber(objectNumber);
    }

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        // ポーズ中は早期リターン
        if (isPause == true) { return; }

        if (objectNumber == 0)
        {
            objectNumber += maxObjectNumber - 1;
        }
        else
        {
            objectNumber--;
        }
        // 照準移動
        Vector3 targetWorldPos = gimmicks[objectNumber].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;
        ObjectNumber(objectNumber);
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        // ポーズ中は早期リターン
        if (isPause == true) { return; }

        fixedRotation = gimmicks[objectNumber].GetComponent<FixedRotation>();
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
        // ポーズ中は早期リターン
        if (isPause == true) { return; }

        //if(gimmicks[objectNumber].TryGetComponent(out FixedRotation fixedRotation))
        //{
        //    fixedRotation.LeftRotate(true, true);
        //}

        fixedRotation = gimmicks[objectNumber].GetComponent<FixedRotation>();
        if (fixedRotation)
        {
            fixedRotation.LeftRotate(true, true);
        }
    }

    public int ObjectNumber(int number)
    {
        objectNumber = number;
        return objectNumber;
    }
}