// 作成日:02/15日水曜日 作成者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// 右スティックの傾きでギミックを選択できるようにするスクリプト
/// </summary>
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(GimmickList))]
[RequireComponent(typeof(RB_LB_GimmickSelect))]
public class RightStick_GimmickSelection : MonoBehaviour
{
    // ギミックを保管する配列
    private GameObject[] gimmicks;
    private GimmickList gimmickList;
    // 現在選択中のギミックの番号
    private int currentSelectObjectNumber;
    // 現在選択中のギミック
    private GameObject currentSelectGimmick;
    // 右スティックの入力ベクトル
    private Vector2 rightStickValue;
    // 選択変更フラグ
    private bool canSelectChanging = true;
    // 計算した角度を保存するリスト
    private List<float> angles;
    // 選択中以外のオブジェクトを保存するリスト
    private List<GameObject> gimmickObjects;
    // エイム画像を入れたオブジェクト
    private Transform aimImageTransform;
    private RectTransform aimRect;
    private Camera mainCamera;
    private PlayerInput playerInput;

    public event Action<int> CurrentObjectNumber;

    private void Start()
    {
        // 孫のtransformを取得
        aimImageTransform = transform.Find("aimImageCanvas/aimImage").gameObject.transform;

        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();
        aimRect = aimImageTransform.parent.GetComponent<RectTransform>();
        mainCamera = Camera.main;

        // ギミック取得
        gimmicks = gimmickList.gimmickLists;

        // デリゲート登録
        playerInput.onActionTriggered += RightStickDegree;

        AimImageMove();

        // 現在選択中のギミックの初期化
        currentSelectGimmick = gimmicks[0];
        CurrentObjectNumber?.Invoke(0);
    }

    /// <summary>
    /// 右スティックの角度を取得する
    /// </summary>
    private void RightStickDegree(InputAction.CallbackContext context)
    {
        // スティック入力以外か選択変更フラグがfalseの時早期リターン
        if (context.action.name != "RightStick" || !canSelectChanging) { return; }
        canSelectChanging = false;

        // 右スティックの角度を取得
        rightStickValue = context.ReadValue<Vector2>();
        // 傾き角度が0以外の時ソートメソッドを実行
        if (rightStickValue != Vector2.zero) { CalculateAngle(); }
        else { canSelectChanging = true; }
    }

    /// <summary>
    /// 角度を計算するメソッド
    /// </summary>
    private void CalculateAngle()
    {
        // リストの初期化
        angles = new List<float>();
        gimmickObjects = new List<GameObject>();

        // 選択中ギミックとそれ以外との角度を求める
        foreach (GameObject t in gimmicks)
        {
            if (currentSelectGimmick.name != t.name)
            {
                // ギミックをリストに追加
                gimmickObjects.Add(t.gameObject);
                // 角度を求めてリストに追加
                angles.Add(Mathf.Abs(Vector2.SignedAngle(rightStickValue, t.transform.position - currentSelectGimmick.transform.position)));
            }
        }

        MinAngleSort();
    }

    /// <summary>
    /// 最小角度をソートするメソッド
    /// </summary>
    private async void MinAngleSort()
    {
        // 最小角度をソート
        float minAngle = angles.Min();
        // 最小角度が何番目のオブジェクトか調べる
        currentSelectGimmick = gimmickObjects[angles.IndexOf(minAngle)];
        // 配列だとIndexOfが使えなかったので一時的にリストに追加し選択しているオブジェクトの番号を調べ引数に渡す
        List<GameObject> obj = gimmicks.ToList();
        currentSelectObjectNumber = obj.IndexOf(currentSelectGimmick);
        CurrentObjectNumber?.Invoke(currentSelectObjectNumber);

        AimImageMove();

        // 待機する
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        // 選択できるようにする
        canSelectChanging = true;
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
}