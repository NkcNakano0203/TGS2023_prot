using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRaycast : MonoBehaviour
{

    public void Ray(Vector3 rayvec)
    {
        // ���C���΂�
        bool rayHit = Physics.Raycast(transform.position, rayvec, out RaycastHit hit);
        // ���C��`��
        Debug.DrawRay(transform.position, rayvec, Color.red);

        // ���C���������Ă��Ȃ��Ƃ��̓��^�[��
        if (!rayHit)
            return;

        // ���C�����������Q�[���I�u�W�F�N�g��IRayRecevier�������Ă�����
        if (!hit.collider.gameObject.TryGetComponent(out IRayRecevier rayRecevier))
            return;

        rayRecevier.Hit(rayvec);

    }
}
