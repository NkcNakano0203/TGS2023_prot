using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    float raylength = float.MaxValue;    

    Ray ray;
    RaycastHit hit;   

    //���C��Ԃ�
    public Ray GetRay() { return ray; }
    //�����������W��ϊ�
    public RaycastHit GetHit() { return hit; }


    //���C���΂�����
    public void RayShot(Vector3 origin, Vector3 direction)
    {       
        Physics.Raycast(origin,direction*raylength, out hit );
        Debug.Log(Physics.Raycast(origin, direction * raylength, out hit));
        Debug.DrawRay(origin, direction * raylength, Color.red);

        RayHit();


    }   
    void RayHit()
    {    
        //�v���C���[�ɓ���������
        if (hit.collider.gameObject.TryGetComponent<PlayerMove>(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }
    }
}
