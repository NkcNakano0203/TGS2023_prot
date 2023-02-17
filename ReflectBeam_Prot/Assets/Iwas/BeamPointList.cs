using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPointList
{
    List<Vector3> linePos = new List<Vector3>();


    public void AddList(Vector3 middlePos)
    {
        Debug.Log("’Ç‰Á‚·‚é‚æ");
        linePos.Add(middlePos);
    }
    public void RemoveList(Vector3 middlePos)
    {
        linePos.Remove(middlePos);
    }

    public int GetCountList()
    {
        return linePos.Count;
    }

    public Vector3[] GetList()
    {
        return linePos.ToArray();
    }

}
