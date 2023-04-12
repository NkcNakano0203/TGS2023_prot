// 作成日:03/20日 作成者:市瀬
using UnityEngine;

public class AimImage : MonoBehaviour
{
    [SerializeField]
    private string aimImagePath = "aimImageCanvas_2/aimImage_2";
    private Transform aimImageTransform;
    private RectTransform aimRect;

    private void Start()
    {
        GameObject aimImage = GameObject.Find(aimImagePath).gameObject;
        aimImageTransform = aimImage.transform;
        aimRect = aimImage.GetComponent<RectTransform>();
    }

    /// <summary>
    /// 照準画像の移動
    /// </summary>
    public void AimImageMove(Transform targetTransform)
    {
        aimImageTransform.position = targetTransform.position;
    }

    /// <summary>
    /// 照準画像の回転メソッド
    /// </summary>
    public void AimImageRotation(Transform targetTransform)
    {
        aimImageTransform.rotation = targetTransform.rotation;
    }
}