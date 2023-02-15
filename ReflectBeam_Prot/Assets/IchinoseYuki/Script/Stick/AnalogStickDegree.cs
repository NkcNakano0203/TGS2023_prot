// 作成日:02/15 作成者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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
    private GameObject nowSelectGimick;

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

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Stick"].performed += StickDegree;
        // 今選択中のギミックの初期化
        nowSelectGimick = gimicks[0];
        nowSelectGimick.GetComponent<MeshRenderer>().material = red;
    }

    private void Update()
    {

    }

    private void StickDegree(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();

        float degree = Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg;
        if (degree < 0)
        {
            degree += 360;
        }

        //Debug.Log(degree);
        SortSelect();
    }

    // ソートするメソッド
    private void SortSelect()
    {
        nowSelectGimick.GetComponent<MeshRenderer>().material = white;
        // リストの初期化
        angles = new List<float>();
        objects = new List<GameObject>();

        foreach (GameObject t in gimicks)
        {
            if (nowSelectGimick.name != t.name)
            {
                objects.Add(t.gameObject);
                Vector3 targetDir = t.transform.position - nowSelectGimick.transform.position;
                // なす角を計算してリストに保存
                angles.Add(Vector3.SqrMagnitude(Vector3.ProjectOnPlane(targetDir, Vector3.forward)));
                Debug.Log(Vector3.SqrMagnitude(Vector3.ProjectOnPlane(targetDir, Vector3.forward)));
            }
        }

        for (int i = 0; i < angles.Count; ++i)
        {
            if(i == 0)
            {
                minAngle = angles[i];
                temporaryGimick = gimicks[i];
            }

            if (minAngle > angles[i] && angles[i] != 0)
            {
                minAngle = angles[i];
                temporaryGimick = gimicks[i];
            }
        }

        nowSelectGimick = temporaryGimick;
        nowSelectGimick.GetComponent<MeshRenderer>().material = red;
    }
}