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
    /// �X�e�[�W�̓ǂݍ��݁A������
    /// </summary>
    /// <param name="stageNum">
    /// �X�e�[�W�ԍ�
    /// </param>
    void StageLoad(int stageNum)
    {
        Instantiate(stages[stageNum], Vector3.zero, Quaternion.identity);
    }
}