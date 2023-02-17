using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRayRecevier2
{
    void RayEnter(Vector3 hitpos, Vector3 rayVec);
    void RayExit();
}