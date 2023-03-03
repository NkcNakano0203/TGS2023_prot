using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorAlpha : MonoBehaviour, IRayReceiverAlpha
{
    [SerializeField]
    InstantiateLaser instantLaser;

    [SerializeField]
    float protectAngle;

    RayShot rayShot;

    List<GameObject> lasers = new List<GameObject>();
   
    int count = 0;

    public void RayEnter(Laser laser)
    {

        Vector3 startPos = laser.GetStartPos();
        Vector3 direction = laser.GetDirection();
        rayShot = laser.GetRayShot();


        // �x�N�g�����擾
        Vector3 dir = direction;

        // ���C�̃x�N�g���𐳋K��
        Vector3 nor_direction = dir.normalized;

        // �@�����擾
        Vector3 normal = transform.up;

        // �@���ƃ��C�̃x�N�g���̓���
        float dot = Vector3.Dot(nor_direction, normal);

        // ��Βldot �����˂ł��Ȃ��p�x��菬���������� false 
        bool isProtect = Mathf.Abs(dot) < protectAngle * Mathf.Deg2Rad;
        if (isProtect)
            return;

        // dot ���}�C�i�X�l��������@���𔽓]������
        Vector3 inNormal = dot < 0 ? -normal : normal;

        // �󂯎�����x�N�g���𔽎˂�����
        Vector3 reflectVec = Vector3.Reflect(dir, inNormal);

        // �@����`�ʂ���
        Debug.DrawRay(transform.position, inNormal, Color.blue);

        Laser nextLaser = new Laser(laser.GetColor(), startPos, reflectVec, null, true);

        // ���C����������
        Vector3 hitPos = rayShot.RaycastShot(nextLaser);

        // ���[�U�[�𐶐����Ă悢��
        if (laser.GetIsInstantLaser())
        {
            // ����
            GameObject obj = instantLaser.AddList_LaserObjs();
            lasers.Add(obj);
        }

        if (count > lasers.Count - 1)
        {
            count = 0;
        }
        // ���[�U�[��transform��ύX
        instantLaser.LaserTransform(startPos,hitPos, lasers[count]);

        count++;
    
        return;
    }

    public void RayExit(Laser laser)
    {
        // rayShot �� null �������烊�^�[��
        if (laser.GetRayShot() == null)
        {
            return;
        }
        // ���C���������Ă��Ȃ��������Ă�
        rayShot.RayExit();
        lasers.RemoveAt(0);
        //lasers.Clear();
        // ���[�U�[���폜����
        instantLaser.RemoveList_LaserObjs();
    }
}