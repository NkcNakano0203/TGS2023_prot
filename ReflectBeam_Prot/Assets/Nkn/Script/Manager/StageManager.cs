using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    StageList stageList;

    Stage[] stages => stageList.GetList;

    int currentStageNum = 0;

    void Start()
    {
        StageLoad(currentStageNum);

    }

    void Update()
    {

    }

    /// <summary>
    /// ステージの読み込み、初期化
    /// </summary>
    /// <param name="stageNum">
    /// ステージ番号
    /// </param>
    void StageLoad(int stageNum)
    {
        Instantiate(stages[stageNum], Vector3.zero, Quaternion.identity);
    }
}