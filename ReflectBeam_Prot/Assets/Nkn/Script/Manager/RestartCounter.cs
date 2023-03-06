using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestartCounter")]
public class RestartCounter : ScriptableObject
{
    [SerializeField]
    private int value;

    public int GetCount => value;

    /// <summary>
    /// リスタート回数を加算する
    /// </summary>
    /// <param name="value"></param>
    public void Add(int value = 1)
    {
        this.value += value;
    }

    /// <summary>
    /// 数を０に初期化する
    /// </summary>
    public void Reset()
    {
        value = 0;
    }
}