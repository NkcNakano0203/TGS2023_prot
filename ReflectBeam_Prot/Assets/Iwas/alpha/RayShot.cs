using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShot
{
    [SerializeField]
    private InstantiateLaser instantiateLaser;

    // �Ō�ɓ����������t���N�^�[��ۑ�����ϐ�
    GameObject lastHitReflector;

    // �ۑ�����ϐ�
    RayShot rayShot;

    // ���[�U�[�̐����̋���
    bool isAddLeser = true;
    // ���[�U�[�̍폜�̋���
    bool isRemoveLaser = false;

    Laser nextLaser;

    public Vector3 RaycastShot(Laser laser)
    {
        Vector3 rayStartPos = laser.GetStartPos();
        Vector3 rayDirection = laser.GetDirection();


        // ���C���������Ă��Ȃ������烊�^�[��
        if (!Physics.Raycast(rayStartPos, rayDirection, out RaycastHit hit))
            return Vector3.zero;

        // ���C�̕`��
        float maxDistance = (hit.point - rayStartPos).magnitude;
        Debug.DrawRay(rayStartPos, rayDirection * maxDistance, laser.GetColor());

        // �����������̂� IRayReceiver �������Ă��邩
        if (hit.collider.gameObject.TryGetComponent(out IRayReceiverAlpha rayReceiver))
        {
            if (isAddLeser)
            {
                rayShot = new RayShot();

                nextLaser = new Laser(laser.GetColor(), hit.point, rayDirection, rayShot, isAddLeser);
                // IRayReceiver �� RayEnter ���Ăяo��
                rayReceiver.RayEnter(nextLaser);
                isAddLeser = false;
            }
            else
            {
                nextLaser = new Laser(laser.GetColor(), hit.point, rayDirection, rayShot, isAddLeser);
                // IRayReceiver �� RayEnter ���Ăяo��
                rayReceiver.RayEnter(nextLaser);
            }
            isRemoveLaser = true;
            // �Ō�ɓ����������t���N�^�[���擾
            lastHitReflector = hit.collider.gameObject;
        }
        else
        {
            isAddLeser = true;
            if (lastHitReflector != null && isRemoveLaser)
            {
                isRemoveLaser = false;
                lastHitReflector.TryGetComponent(out IRayReceiverAlpha lastHit);

                lastHit.RayExit(nextLaser);
            }
        }

        //�v���C���[�ɓ���������
        if (hit.collider.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove.PlayerDeath();
        }

        return hit.point;
    }
    public void RayExit()
    {
        // ���[�U�[�̐�����������
        isAddLeser = true;
        // ������
        rayShot = null;
        // �Ō�ɓ����������t���N�^�[������
        // ����
        // ���[�U�[���폜���鋖������ꍇ
        if (lastHitReflector != null && isRemoveLaser)
        {
            // ���[�U�[�̍폜�������Ȃ�
            isRemoveLaser = false;
            // �Ō�ɓ����������t���N�^�[�̏������Ă�
            lastHitReflector.TryGetComponent(out IRayReceiverAlpha lastHit);
            lastHit.RayExit(nextLaser);
        }
    }
}