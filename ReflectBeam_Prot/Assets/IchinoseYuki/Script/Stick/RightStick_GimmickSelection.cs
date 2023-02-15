// 作成日:02/15日水曜日 作成者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// 右スティックの傾きでギミックを選択できるようにするスクリプト
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class RightStick_GimmickSelection : MonoBehaviour
{
    [SerializeField, Header("ギミックを保管するリスト")]
    private List<GameObject> gimmicks;

    // 現在選択中のギミック
    [SerializeField]
    private GameObject currentSelectGimmick;

    // 角度を保存するリスト
    private List<float> angles;

    // 比較するギミックを保存しておくリスト
    private List<GameObject> gimmickObjects;

    // 一時的にギミックを保存しておく変数
    private GameObject temporaryGimick;

    // 最小の角度を保存する変数
    private float minAngle;

    // 赤マテリアル
    [SerializeField]
    private Material red;

    // 白マテリアル
    [SerializeField]
    private Material white;

    // 右スティックの入力ベクトルを保存する変数
    private Vector2 rightStickValue;

    // 選択を変えることができるか判定するフラグ
    private bool isSelect = true;

    PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // デリゲート登録
        playerInput.onActionTriggered += RightStickDegree;

        // 現在選択中のギミックの初期化
        currentSelectGimmick = gimmicks[0];
        currentSelectGimmick.GetComponent<MeshRenderer>().material = red;
    }

    /// <summary>
    /// 右スティックの角度を取得する
    /// </summary>
    private void RightStickDegree(InputAction.CallbackContext context)
    {
        if (!isSelect) { return; }
        isSelect = false;

        // 角度を取得
        rightStickValue = context.ReadValue<Vector2>();

        // 値が0以外の時
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
        // 前に選択していた色を白にする
        currentSelectGimmick.GetComponent<MeshRenderer>().material = white;

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
        minAngle = 1000f;
        // ソート
        for (int i = 0; i < angles.Count; ++i)
        {
            if (minAngle > angles[i] && angles[i] != 0)
            {
                minAngle = angles[i];
                temporaryGimick = gimmickObjects[i];
            }
        }

        currentSelectGimmick = temporaryGimick;
        currentSelectGimmick.GetComponent<MeshRenderer>().material = red;

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        isSelect = true;
    }
}