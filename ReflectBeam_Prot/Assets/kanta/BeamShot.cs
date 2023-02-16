using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamShot : MonoBehaviour
{
    [SerializeField]
    BeamDraw beamDraw;

    GameObject hitObj;
    RaycastHit raycastHit;
    LastHit lastHit;

    private void Update()
    {
        if (hitObj == null)
        {
            return;
        }
        Debug.Log(lastHit.lastHitObj);
        if (hitObj != lastHit.lastHitObj)
        {
            if (hitObj.TryGetComponent(out IRayRecevier rayRecevier))
            {
                rayRecevier.RayExit();
            }
        }
    }


    // ���C���o�������ʒu,���C���΂������x�N�g��
    //���C���΂�����(�����ł��o���������Ăяo��)
    public Vector3 RayShot(Vector3 origin, Vector3 direction)
    {
        //raylength�����������I�[�o�[�t���[�����Ȃ�����
        direction = direction.normalized;


        RaycastHit[] hits = Physics.RaycastAll(origin, direction);
        Debug.DrawRay(origin, direction, Color.red);


        foreach (var item in hits)
        {
            //Ray�������������̏���        
            raycastHit = item;

            // �����������̂�������������continue
            if (item.collider.gameObject == gameObject)
                continue;

            //�v���C���[�ɓ���������
            if (item.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
            {
                playerMove.PlayerDeath();
                break;
            }

            //���˕��ɓ���������
            if (item.collider.gameObject.TryGetComponent(out IRayRecevier irayRecevier))
            {
                hitObj = item.collider.gameObject;
                irayRecevier.RayEnter(direction, item.point);

               bool hoge = item.collider.gameObject.TryGetComponent(out Reflector reflector);

                //

                //

                
                reflector.SetAction(beamDraw.AddList);

                break;
            }
        }

        lastHit = new LastHit(raycastHit.collider.gameObject);
        return raycastHit.point;
    }
}
