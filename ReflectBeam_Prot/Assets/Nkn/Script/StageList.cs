using UnityEngine;
using System.Linq;

[CreateAssetMenu()]
public class StageList : ScriptableObject
{
    [SerializeField]
    Stage[] stageList;

    public Stage[] GetList => stageList.ToArray();
}