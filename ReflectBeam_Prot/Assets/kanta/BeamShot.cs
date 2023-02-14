using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    [SerializeField]float raylength = 10;
  
    // Update is called once per frame
  
    public void RayShot()
    {
        Vector3 origin = transform.position; // ���_
        Vector3 direction = Vector3.right*raylength;   //�����Ă������

        Ray ray = new Ray(origin, direction); // Ray�𐶐�;

        Physics.Raycast(ray, raylength);
        Debug.DrawRay(origin, direction, Color.red);

    }
}
