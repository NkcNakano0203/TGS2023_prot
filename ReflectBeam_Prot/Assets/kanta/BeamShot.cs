using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{

    [SerializeField]
    BeamDrow beamDrow;

    Ray ray;
    RaycastHit hit;

    //���C��Ԃ�
    public Ray GetRay() { return ray; }
    //�����������W��ϊ�
    public RaycastHit GetHit() { return hit; }


    // ���C���o�������ʒu,���C���΂������x�N�g��
    //���C���΂�����(�����ł��o���������Ăяo��)
    public void RayShot(Vector3 origin, Vector3 direction,bool isDrow)
    {
        //raylength�����������I�[�o�[�t���[�����Ȃ�����
        direction = direction.normalized;


        Physics.Raycast(origin, direction, out hit);

        //Ray�������������̏���        
        RayHit(hit, direction);


        beamDrow.DrowShot(hit.point,origin,isDrow);
    }

    // ���������|�C���g,���C�̕����x�N�g��
    void RayHit(RaycastHit hit, Vector3 direction)
    {
        //�������ĂȂ��Ƃ����^�[��
        if (hit.collider == null) { return; }

        //�v���C���[�ɓ���������
        if (hit.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }

        //���˕��ɓ���������
        if (hit.collider.gameObject.TryGetComponent(out IRayRecevier irayRecevier))
        {
            irayRecevier.Hit(direction, hit.point, hit);            
        }
    }
}
