using UnityEngine;
class GimmickUISize
{
    // ����̍��W
    Vector2 leftTop;
    // �E���̍��W
    Vector2 rightBottom;

    // ���S���W
    Vector2 pos;

    public GimmickUISize(Vector2 leftTop,Vector2 rightBottom,Vector2 position)
    {
        this.leftTop = leftTop;
        this.rightBottom = rightBottom;
        this.pos = position;
    }
}
