using UnityEngine;
interface IGimmick
{
    // �M�~�b�N�������������ɓ�������
    public Vector3 OnHit(Vector3 vec);

    // �M�~�b�N�̃T�C�Y���擾���鏈��
    public GimmickUISize GetSize();
}
