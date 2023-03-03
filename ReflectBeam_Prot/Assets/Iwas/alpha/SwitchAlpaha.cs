using UnityEngine;

public class SwitchAlpaha : MonoBehaviour, IRayReceiverAlpha
{
    [SerializeField]
    bool isSetColor;

    [SerializeField]
    string colorName;

    bool isHit = false;

    public void RayEnter(Laser laser)
    {
        if (isSetColor)
        {
            Hit(laser.GetColor());
        }
        else
        {
            Hit();
        }

    }

    public void RayExit(Laser laser)
    {
        isHit = false;
    }

    private void Hit(Color laserColor)
    {
        if (!ColorUtility.TryParseHtmlString(colorName, out Color color))
        {
            Debug.Log("変換できません");
        }

        if (color == laserColor)
        {
            Debug.Log("ゴールできるよ！！");
            Debug.Log(color == laserColor);
            isHit = true;
        }
    }

    private void Hit()
    {
        Debug.Log("ゴールできるよ！！");
        isHit = true;
    }

    public bool GetIsHit() { return isHit; }
}
