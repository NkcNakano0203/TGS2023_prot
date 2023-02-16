
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRayRecevier
{
    LastHit Hit(Vector3 rayVec, Vector3 rayPos, RaycastHit hit);
}
