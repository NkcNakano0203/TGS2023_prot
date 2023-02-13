using UnityEngine;
interface IGimmick
{
    // ギミックが当たった時に動く処理
    public Vector3 OnHit(Vector3 vec);

    // ギミックのサイズを取得する処理
    public GimmickUISize GetSize();
}
