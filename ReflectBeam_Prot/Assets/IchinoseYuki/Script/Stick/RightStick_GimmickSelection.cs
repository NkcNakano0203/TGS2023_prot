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
public class RightStick_GimmickSelection : MonoBehaviour,IObjectNumber
{
    // ギミックを保管する配列
    private GameObject[] gimmicks;
    private GimmickList gimmickList;

    // オブジェクトの順番
    private int objectNumber;

    // 現在選択中のギミック
    [SerializeField]
    private GameObject currentSelectGimmick;

    // 角度を保存するリスト
    private List<float> angles;

    // 比較するギミックを保存しておくリスト
    private List<GameObject> gimmickObjects;

    // 一時的にギミックを保存しておく変数
    //private GameObject temporaryGimick;

    // 最小の角度を保存する変数
    [SerializeField]
    private float minAngle;
    // 右スティックの入力ベクトルを保存する変数
    private Vector2 rightStickValue;

    // 選択を変えることができるか判定するフラグ
    private bool isSelect = true;

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

        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();
        aimRect = aimImage.transform.parent.GetComponent<RectTransform>();
        mainCamera = Camera.main;

        // ギミック取得
        gimmicks = gimmickList.gimmickLists;

        // デリゲート登録
        playerInput.onActionTriggered += RightStickDegree;

        // 照準移動
        var targetWorldPos = gimmicks[0].transform.position;
        var targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(aimRect, targetScreenPos, null, out var uiLocalPos);
        aimImage.transform.localPosition = uiLocalPos;

        // 現在選択中のギミックの初期化
        currentSelectGimmick = gimmicks[0];
    }

    /// <summary>
    /// 右スティックの角度を取得する
    /// </summary>
    private void RightStickDegree(InputAction.CallbackContext context)
    {
        // スティック入力以外はリターン
        if(context.action.name != "RightStick") { return; }
        // 選択を変更できるか判断するフラグがfalseの時はリターン
        if (!isSelect) { return; }
        isSelect = false;

        // 右スティックの角度を取得
        rightStickValue = context.ReadValue<Vector2>();

        // 傾き角度が0以外の時
        if (rightStickValue != Vector2.zero)
        {
            // ソートメソッドを実行
            SortSelect();
        }
        else
        {
            // 選択できるようにする
            isSelect = true;
        }
    }

    /// <summary>
    /// ソートするメソッド
    /// </summary>
    private async void SortSelect()
    {
        // リストの初期化
        angles = new List<float>();
        gimmickObjects = new List<GameObject>();

        // 現在選択しているギミックをそれ以外と比較して角度を求める
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

        // 比較用に大きい数字を代入しておく
        //minAngle = 1000f;
        // ソート
        /*
        for (int i = 0; i < angles.Count; ++i)
        {
            if (minAngle > angles[i] && angles[i] != 0)
            {
                minAngle = angles[i];
                temporaryGimick = gimmickObjects[i];
            }
        }
        */

        // Linqって便利！
        // 最小角度をソートして代入
        minAngle = angles.Min();
        // 最小角度が何番目のオブジェクトか調べて一時保存変数に入れる
        currentSelectGimmick = gimmickObjects[angles.IndexOf(minAngle)];
        objectNumber = angles.IndexOf(minAngle);
        ObjectNumber(objectNumber);


        var targetWorldPos = currentSelectGimmick.transform.position;

        var targetScreenPos = mainCamera.WorldToScreenPoint(targetWorldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            aimRect,
            targetScreenPos,
            null,
            out var uiLocalPos
            );
        aimImage.transform.localPosition = uiLocalPos;

        // 待機する
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        // 選択できるようにする
        isSelect = true;
    }

    public int ObjectNumber(int number)
    {
        objectNumber = number;
        return objectNumber;
    }
}