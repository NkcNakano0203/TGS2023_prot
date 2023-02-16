using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{

    [SerializeField]
    BeamDrow beamDrow;

    bool isDrow;
    Vector3 startPos;

    GameObject hitObj;

    Ray ray;
    RaycastHit hit;

    LastHit lastHit;

    //���C��Ԃ�
    public Ray GetRay() { return ray; }
    //�����������W��ϊ�
    public RaycastHit GetHit() { return hit; }


    private void Update()
    {
        if (hitObj == null)
        {
            Debug.Log("a");
            return;
        }

        if (hitObj != lastHit.lastHitObj)
        {
            if (hitObj.TryGetComponent(out IRayRecevier rayRecevier))
            {
                rayRecevier.NoHit();
            }
        }
    }


    // ���C���o�������ʒu,���C���΂������x�N�g��
    //���C���΂�����(�����ł��o���������Ăяo��)
    public Vector3 RayShot(Vector3 origin, Vector3 direction, bool isDrow)
    {
        //raylength�����������I�[�o�[�t���[�����Ȃ�����
        direction = direction.normalized;


        Physics.Raycast(origin, direction, out hit);

        //Ray�������������̏���        
        RayHit(hit, direction);
        this.isDrow = isDrow;
        startPos = origin;
        lastHit = new LastHit(hit.collider.gameObject);
        return hit.point;
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
            hitObj = hit.collider.gameObject;
            irayRecevier.Hit(direction, hit.point, hit);
        }

    }
}
