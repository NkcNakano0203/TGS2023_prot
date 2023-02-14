// 作成日02/14日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// リモコンスクリプト_2
/// </summary>
public class RemoteControl_2 : MonoBehaviour
{
    [SerializeField, Header("レフレクターやビームなど全てのギミックを保管するリスト")]
    private List<GameObject> gimicks;

    private PlayerInput playerInput;

    // ギミックの順番
    private int gimickNumber;
    // 最大ギミック数
    private int maxGimicks;

    [SerializeField]
    private Material red;
    
    [SerializeField]
    private Material white;

    // 今選択中のギミック
    private GameObject nowSelectGimick;
    // 仮置き変数
    private GameObject temporaryGimick;
    /// <summary>
    /// tとの距離
    /// </summary>
    private float tDistance;
    // 
    [SerializeField]
    List<GameObject> rightSideGimicks;
    // 
    [SerializeField]
    List<GameObject> leftSideGimicks;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // デリゲート登録
        playerInput.actions["RemoteControl_R_Shoulder"].performed += On_R_ShoulderButton;
        playerInput.actions["RemoteControl_L_Shoulder"].performed += On_L_ShoulderButton;
        playerInput.actions["RemoteControl_R_Trigger"].performed += On_R_TriggerButton;
        playerInput.actions["RemoteControl_L_Trigger"].performed += On_L_TriggerButton;

        gimickNumber = 0;
        maxGimicks = gimicks.Count - 1;
        gimicks[gimickNumber].GetComponent<MeshRenderer>().material = red;
        nowSelectGimick = gimicks[gimickNumber].gameObject;
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void On_R_ShoulderButton(InputAction.CallbackContext context)
    {
        // 選択中のギミックの色を白にする
        nowSelectGimick.GetComponent<MeshRenderer>().material = white;

        // 選択されているものより右側のギミックを探し出すメソッドを実行
        RightSide();

        // 仮の初期値
        float temporaryDistance = 1000;
        foreach (GameObject t in rightSideGimicks)
        {
            // 距離を可視化
            //Debug.Log(Vector3.Distance(nowSelectGimick.transform.position, t.transform.position), this.gameObject);

            // 対象のギミックとの距離を計算
            tDistance = Vector3.SqrMagnitude(nowSelectGimick.transform.position - t.transform.position);

            // 仮初期値より距離が近いとき
            if(temporaryDistance > tDistance)
            {
                // 仮初期値を上書き
                temporaryDistance = tDistance;
                // 仮置きギミックをtギミックで上書き
                temporaryGimick = t;
            }
        }

        // 選択中のギミックを仮置きギミックで上書き
        nowSelectGimick = temporaryGimick;

        // 選択中のギミックの色を赤にする
        nowSelectGimick.GetComponent<MeshRenderer>().material = red;
        Debug.Log(nowSelectGimick.name);
    }

    /// <summary>
    /// 選択されているものより右側のギミックを探し出すメソッド
    /// </summary>
    private void RightSide()
    {
        // リストの初期化
        rightSideGimicks = new List<GameObject>();

        // 今選択されているギミックよりX軸が大きいギミックは新しいListに追加
        foreach (GameObject t in gimicks)
        {
            if(nowSelectGimick.transform.position.x < t.transform.position.x)
            {
                rightSideGimicks.Add(t);
            }
        }
    } 

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void On_L_ShoulderButton(InputAction.CallbackContext context)
    {
        // 選択中のギミックの色を白にする
        nowSelectGimick.GetComponent<MeshRenderer>().material = white;

        // 選択されているものより左側のギミックを探し出すメソッドを実行
        LeftSide();

        // 仮の初期値
        float temporaryDistance = 1000;
        foreach (GameObject t in leftSideGimicks)
        {
            // 距離を可視化
            //Debug.Log(Vector3.Distance(nowSelectGimick.transform.position, t.transform.position), this.gameObject);

            // 対象のギミックとの距離を計算
            tDistance = Vector3.SqrMagnitude(nowSelectGimick.transform.position - t.transform.position);

            // 仮初期値より距離が近いとき
            if (temporaryDistance > tDistance)
            {
                // 仮初期値を上書き
                temporaryDistance = tDistance;
                // 仮置きギミックをtギミックで上書き
                temporaryGimick = t;
            }
        }

        // 選択中のギミックを仮置きギミックで上書き
        nowSelectGimick = temporaryGimick;

        // 選択中のギミックの色を赤にする
        nowSelectGimick.GetComponent<MeshRenderer>().material = red;
        Debug.Log(nowSelectGimick.name);
    }

    /// <summary>
    /// 選択されているものより左側のギミックを探し出すメソッド
    /// </summary>
    private void LeftSide()
    {
        // リストの初期化
        leftSideGimicks = new List<GameObject>();

        // 今選択されているギミックよりX軸が小さいギミックは新しいListに追加
        foreach (GameObject t in gimicks)
        {
            if (nowSelectGimick.transform.position.x > t.transform.position.x)
            {
                leftSideGimicks.Add(t);
            }
        }
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void On_R_TriggerButton(InputAction.CallbackContext context)
    {
    }

    /// <summary>
    /// L2ボタン
    /// </summary>
    private void On_L_TriggerButton(InputAction.CallbackContext context)
    {
    }
}