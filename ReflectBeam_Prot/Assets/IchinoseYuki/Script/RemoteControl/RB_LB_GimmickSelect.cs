// 作成日02/17日金曜日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// RBとLBでギミック選択をするスクリプト
/// </summary>
[RequireComponent(typeof(GimmickList))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(RightStick_GimmickSelection))]
public class RB_LB_GimmickSelect : MonoBehaviour
{
    // ギミックを保管する配列
    private GameObject[] gimmicks;
    private GimmickList gimmickList;
    // 現在選択中のギミックの番号
    private int currentSelectObjectNumber;
    // 最大オブジェクト数
    private int maxObjectNumber;

    private bool is_R_Trigger_Pressed;
    private bool is_L_Trigger_Pressed;
    private InputAction leftAction;
    private InputAction rightAction;
    private FreeRotation freeRotation;
    private Camera mainCamera;
    private Transform aimImageTransform;
    private RectTransform aimRect;
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();
        // ギミック取得
        gimmicks = gimmickList.gimmickLists;
        maxObjectNumber = gimmicks.Length;
        // イベント登録
        RightStick_GimmickSelection rightStick_GimmickSelection = GetComponent<RightStick_GimmickSelection>();
        rightStick_GimmickSelection.CurrentObjectNumber += CurrentObjectNumber;
        // デリゲート登録
        playerInput.actions["L_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["R_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["R_Trigger"].started += On_R_TriggerButton;
        playerInput.actions["L_Trigger"].started += On_L_TriggerButton;

        // 孫のtransformを取得
        aimImageTransform = transform.Find("aimImageCanvas/aimImage").gameObject.transform;
        aimRect = aimImageTransform.parent.GetComponent<RectTransform>();
        mainCamera = Camera.main;
        AimImageMove();

        rightAction = playerInput.actions["R_Trigger"];
        leftAction = playerInput.actions["L_Trigger"];
    }

    private void Update()
    {
        GimmickFreeRotation();
    }

    /// <summary>
    /// 自由回転時の処理
    /// </summary>
    private void GimmickFreeRotation()
    {
        is_R_Trigger_Pressed = rightAction.IsPressed();
        is_L_Trigger_Pressed = leftAction.IsPressed();

        // RかLのトリガーが押されているとき回転させる
        if (is_R_Trigger_Pressed || is_L_Trigger_Pressed)
        {
            freeRotation = gimmicks[currentSelectObjectNumber].transform.GetChild(0).gameObject.GetComponent<FreeRotation>();
            if (freeRotation)
            {
                freeRotation.RightRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
                freeRotation.LeftRotate(is_L_Trigger_Pressed, is_R_Trigger_Pressed);
            }
        }
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        if (maxObjectNumber - 1 > currentSelectObjectNumber) { currentSelectObjectNumber++; }
        else { currentSelectObjectNumber = 0; }
        AimImageMove();
    }

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        if (currentSelectObjectNumber == 0) { currentSelectObjectNumber += maxObjectNumber - 1; }
        else { currentSelectObjectNumber--; }
        AimImageMove();
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
        if (gimmicks[currentSelectObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.RightRotate(true, true);
        }
    }

    /// <summary>
    /// L2ボタン
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
        if (gimmicks[currentSelectObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.LeftRotate(true, true);
        }
    }

    /// <summary>
    /// 照準画像の移動処理
    /// </summary>
    private void AimImageMove()
    {
        Vector3 targetWorldPos = gimmicks[currentSelectObjectNumber].transform.position;
        Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImageTransform.localPosition = uiLocalPos;
    }

    /// <summary>
    /// 右スティックで選択中オブジェクトを変えたとき何番目にしたかを通知するメソッド
    /// </summary>
    private void CurrentObjectNumber(int number)
    {
        currentSelectObjectNumber = number;
    }
}