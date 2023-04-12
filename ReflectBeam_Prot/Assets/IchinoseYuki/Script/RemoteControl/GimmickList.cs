// 作成日:02/17日金曜日 作成者:市瀬
using UnityEngine;
using System.Linq;

/// <summary>
/// ギミックをまとめるスクリプト
/// </summary>
[RequireComponent(typeof(RightStick_GimmickSelection))]
public class GimmickList : MonoBehaviour
{
    // ステージの全てのギミックを保存する配列
    [SerializeField]
    private GameObject[] gimmickList;

    public GameObject[] gimmickLists => gimmickList.ToArray();
}