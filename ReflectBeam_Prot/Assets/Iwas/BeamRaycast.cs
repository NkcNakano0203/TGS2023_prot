using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRaycast : MonoBehaviour
{
    public void Ray(Vector3 rayvec,Vector3 rayPos)
    {
        // ���C���΂�
        bool rayHit = Physics.Raycast(rayPos, rayvec, out RaycastHit hit);
        // ���C��`��
        Debug.DrawRay(rayPos, rayvec, Color.red);

        // ���C���������Ă��Ȃ��Ƃ��̓��^�[��
        if (!rayHit)
            return;

        // ���C�����������Q�[���I�u�W�F�N�g��IRayRecevier�������Ă�����
        if (!hit.collider.gameObject.TryGetComponent(out IRayRecevier2 rayRecevier))
            return;
        //Debug.Log(hit.point);

      //  rayRecevier.Hit(rayvec,hit.point);
    }
}
