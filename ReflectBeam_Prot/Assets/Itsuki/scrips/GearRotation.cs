using UnityEngine;

public class GearRotation : MonoBehaviour
{
    private Vector3 rotateSpeed = new Vector3(0, 0, 1.5f);
    private void Update()
    {
        transform.Rotate(rotateSpeed);
    }
}