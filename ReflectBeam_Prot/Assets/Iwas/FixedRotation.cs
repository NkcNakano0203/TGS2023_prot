using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FixedRotation : MonoBehaviour, IRotatable
{
    // ��]�����̕ϐ�
    bool isRotate = false;

    // ��]����p�x
    readonly float theta = 45f;

    // ��]���鎞��
    [SerializeField]
    float rotateTime;

  
    // �E��]�i�C���^�[�t�F�[�X�j
    public void RightRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ��]���Ȃ瑁�����^�[��
        if (isRotate)
            return;

        // THETA����]����
        transform.DOLocalRotate(new Vector3(0, 0, -theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ��]���ɂ���
        isRotate = true;
    }

    public void LeftRotate(bool isLeftRotate, bool isRightRotate)
    {
        // ��]���Ȃ瑁�����^�[��
        if (isRotate)
            return;

        // rotateTime������THETA����]����
        transform.DOLocalRotate(new Vector3(0, 0, theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ��]���ɂ���
        isRotate = true;
    }
}
