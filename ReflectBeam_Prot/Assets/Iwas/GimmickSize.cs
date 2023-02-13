using UnityEngine;
class GimmickUISize
{
    // 左上の座標
    Vector2 leftTop;
    // 右下の座標
    Vector2 rightBottom;

    // 中心座標
    Vector2 pos;

    public GimmickUISize(Vector2 leftTop,Vector2 rightBottom,Vector2 position)
    {
        this.leftTop = leftTop;
        this.rightBottom = rightBottom;
        this.pos = position;
    }
}
