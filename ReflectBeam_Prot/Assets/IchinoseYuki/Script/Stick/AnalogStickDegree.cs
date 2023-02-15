// 作成日:02/15 作成者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// アナログスティックの傾き角度を取得するスクリプト
/// </summary>
public class AnalogStickDegree : MonoBehaviour
{
    [SerializeField, Header("レフレクターやビームなど全てのギミックを保管するリスト")]
    private List<GameObject> gimicks;

    PlayerInput playerInput;

    // 今選択中のギミック
    [SerializeField]
    private GameObject currentSelectGimick;

    // 角度を保存するリスト
    [SerializeField]
    private List<float> angles;

    // 確認用リスト
    [SerializeField]
    private List<GameObject> objects;

    // 仮置き変数
    private GameObject temporaryGimick;

    // 最小の角度
    [SerializeField]
    private float minAngle;

    [SerializeField]
    private Material red;

    [SerializeField]
    private Material white;

    Vector2 value;

    private bool isSelect = true;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += StickDegree;
        // 今選択中のギミックの初期化
        currentSelectGimick = gimicks[0];
        currentSelectGimick.GetComponent<MeshRenderer>().material = red;
    }

    private void Update()
    {
        Debug.Log(isSelect);
    }

    private void StickDegree(InputAction.CallbackContext context)
    {
        if (!isSelect) { return; }
        isSelect = false;

        value = context.ReadValue<Vector2>();
        //Debug.Log(value);

        //float degree = Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg;
        //if (degree < 0)
        //{
        //    degree += 360;
        //}

        //Debug.Log(degree);
        if (value != Vector2.zero)
        {
            SortSelect();
        }
        else
        {
            isSelect = true;
        }
    }

    // ソートするメソッド
    private async void SortSelect()
    {

        currentSelectGimick.GetComponent<MeshRenderer>().material = white;

        // リストの初期化
        angles = new List<float>();
        objects = new List<GameObject>();

        foreach (GameObject t in gimicks)
        {
            if (currentSelectGimick.name != t.name)
            {
                objects.Add(t.gameObject);
                //Vector3 targetDir =  t.transform.position;
                // なす角を計算してリストに保存
                //angles.Add(Vector3.SqrMagnitude(Vector3.ProjectOnPlane(targetDir, Vector3.forward)));
                //angles.Add(Vector2.Angle(t.transform.position, value));
                angles.Add(Mathf.Abs( Vector2.SignedAngle(value, t.transform.position - currentSelectGimick.transform.position)));

                //Debug.Log(Vector3.SqrMagnitude(Vector3.ProjectOnPlane(targetDir, Vector3.forward)));
            }
        }

        minAngle = 10000000f;
        for (int i = 0; i < angles.Count; ++i)
        {
            if (minAngle > angles[i] && angles[i] != 0)
            {
                minAngle = angles[i];
                temporaryGimick = objects[i];
            }
        }

        currentSelectGimick = temporaryGimick;
        currentSelectGimick.GetComponent<MeshRenderer>().material = red;

        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        isSelect = true;
    }
}