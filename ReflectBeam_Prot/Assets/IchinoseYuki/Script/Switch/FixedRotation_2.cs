using UnityEngine;
using DG.Tweening;

public class FixedRotation_2 : MonoBehaviour, IRotatable
{
    // ‰ñ“]’†‚©‚Ì•Ï”
    private bool isRotate = false;

    // ‰ñ“]‚·‚éŠp“x
    const float theta = 45f;

    // ‰ñ“]‚·‚éŽžŠÔ
    [SerializeField]
    private float rotateTime;

    // ‰E‰ñ“]iƒCƒ“ƒ^[ƒtƒF[ƒXj
    public void RightRotate()
    {
        // ‰ñ“]’†‚È‚ç‘ŠúƒŠƒ^[ƒ“
        if (isRotate) { return; }

        // THETA•ª‰ñ“]‚·‚é
        transform.DOLocalRotate(new Vector3(0, 0, -theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ‰ñ“]’†‚É‚·‚é
        isRotate = true;
    }

    public void LeftRotate()
    {
        // ‰ñ“]’†‚È‚ç‘ŠúƒŠƒ^[ƒ“
        if (isRotate) { return; }

        // rotateTime‚©‚¯‚ÄTHETA•ª‰ñ“]‚·‚é
        transform.DOLocalRotate(new Vector3(0, 0, theta), rotateTime)
            .SetRelative(true)
            .OnComplete(() => isRotate = false);

        // ‰ñ“]’†‚É‚·‚é
        isRotate = true;
    }
}