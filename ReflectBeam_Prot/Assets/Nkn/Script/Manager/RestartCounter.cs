using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestartCounter")]
public class RestartCounter : ScriptableObject
{
    [SerializeField]
    private int value;

    public int GetCount => value;

}