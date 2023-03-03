using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser
{
    private readonly Color color;
    private readonly Vector3 startPos;
    private readonly Vector3 hitPos;
    private readonly Vector3 direction;
    private readonly RayShot rayShot;
    private readonly bool isInstantLaser;

    public Laser(Color color, Vector3 startPos,Vector3 direction, RayShot rayShot, bool isInstantLaser)
    {
        this.color = color;
        this.startPos = startPos;
        this.direction = direction;
        this.rayShot = rayShot;
        this.isInstantLaser = isInstantLaser;
    }

    public Color GetColor() { return color; }
    public Vector3 GetStartPos() { return startPos; }
    public Vector3 GetHitPos() { return hitPos; }
    public Vector3 GetDirection() { return direction; }
    public RayShot GetRayShot() { return rayShot; }
    public bool GetIsInstantLaser() { return isInstantLaser; }
};
