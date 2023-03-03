using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDispenser : MonoBehaviour
{
    [SerializeField]
    InstantiateLaser instantiateLaser;

    RayShot rayShot;

    GameObject laserObj;
    Laser laser;

    private void Awake()
    {
        rayShot =  new RayShot();

        laserObj = instantiateLaser.AddList_LaserObjs();
    }

    private void Update()
    {

        // ���C�̔��˒n�_
        Vector3 rayStartPos = transform.position;
        // ���C�̕���
        Vector3 rayDirection = transform.right;

        laser = new Laser(Color.red, rayStartPos, rayDirection, null, true);

        // ���C�����������A�����������W�����炤
        Vector3 hitPos = rayShot.RaycastShot(laser);
        // ���[�U�[���܂��`�悳��Ă��Ȃ�������
  
       // ���[�U�[��transform�̕ύX
        instantiateLaser.LaserTransform(laser.GetStartPos(), hitPos,laserObj);
    }
}