using UnityEngine;
using DG.Tweening;

public class FixedRotation_2 : MonoBehaviour, IRotatable
{
    // ��]�����̕ϐ�
    private bool isRotate = false;

    // ��]����p�x
    const float theta = 45f;

    // ��]���鎞��
    [SerializeField]
    private float rotateTime;

    // �E��]�i�C���^�[�t�F�[�X�j
    public void RightRotate()
    {
        // ��]���Ȃ瑁�����^�[��
        if (isRotate) { return; }

        // THETA����]����
        transform.DOLocalRotate(new Vector3(0, 0, -theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ��]���ɂ���
        isRotate = true;
    }

    public void LeftRotate()
    {
        // ��]���Ȃ瑁�����^�[��
        if (isRotate) { return; }

        // rotateTime������THETA����]����
        transform.DOLocalRotate(new Vector3(0, 0, theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ��]���ɂ���
        isRotate = true;
    }
}