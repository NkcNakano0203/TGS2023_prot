using UnityEngine;

public class SwitchAlpaha : MonoBehaviour, IRayReceiverAlpha
{
    [SerializeField]
    bool isSetColor;

    [SerializeField]
    string colorName;

    bool isHit = false;

    // switch���N������܂ł̎���
    float toStart = 0;

    //[SerializeField]
    //GameObject obj1;

    public void RayEnter(Laser laser)
    {

        if (isHit)
        {
            toStart = 0;
            return;
        }

        toStart += Time.deltaTime;

        

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
            Debug.Log("�ϊ��ł��܂���");
        }

        if (color == laserColor)
        {
            Debug.Log("�S�[���ł����I�I");
            Debug.Log(color == laserColor);
            isHit = true;
        }
    }

    private void Hit()
    {
        Debug.Log("�S�[���ł����I�I");
        isHit = true;
    }

    public bool GetIsHit() { return isHit; }

    //public void Countdown(float toStart)
    //{
    //    if(toStart > 1)
    //    {
    //        obj1.GetComponent<MeshRenderer>().material.e
    //    }

    //}

}
