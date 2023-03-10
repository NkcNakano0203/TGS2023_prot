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
    /// ���X�^�[�g�񐔂����Z����
    /// </summary>
    /// <param name="value"></param>
    public void Add(int value = 1)
    {
        this.value += value;
    }

    /// <summary>
    /// �����O�ɏ���������
    /// </summary>
    public void Reset()
    {
        value = 0;
    }
}