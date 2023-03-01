using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IRayRecevier2
{
    [SerializeField]
    Goal goal;

    public void RayEnter(Vector3 hitpos, Vector3 rayVec)
    {
        //Debug.Log("open", gameObject);
        goal.Open();
    }
    public void RayExit()
    {
        //Debug.Log("close", gameObject);
        goal.Close();
    }

}