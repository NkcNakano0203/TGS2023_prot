
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRayRecevier
{
    LastHit RayEnter(Vector3 rayVec, Vector3 rayPos);
    public void RayExit();
}
