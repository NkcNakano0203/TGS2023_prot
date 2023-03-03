using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRayReceiverAlpha
{
    public void RayEnter(Laser laser);
    public void RayExit(Laser laser);
}