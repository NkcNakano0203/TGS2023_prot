using UnityEngine;

public class SwitchAlpaha : MonoBehaviour, IRayReceiverAlpha,IRayRecevier2
{
    [SerializeField]
    Goal goal;


    [SerializeField, Header("スイッチの光る部位")]
    GameObject[] switchObj;

    [SerializeField, Header("光る間隔時間")]
    float intervalTime;


    [SerializeField, Header("初期化の色")]
    Color color;

    float countDownTime = 0;

    // 光っている数
    int countDown = 0;

    [SerializeField]
    float intensity;


    public void RayEnter(Laser laser)
    {
        
    }

    public void RayEnter(Vector3 hitpos, Vector3 rayVec)
    {
        CountUp(Color.red);
    }

    public void RayExit(Laser laser)
    {
        foreach (var item in switchObj)
        {
            MeshRenderer meshRenderer = item.GetComponent<MeshRenderer>();
            meshRenderer.material.DisableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", color);
            
        }
        countDownTime = 0;
        countDown = 0;
        goal.Close();
    }

    public void RayExit()
    {
        foreach (var item in switchObj)
        {
            MeshRenderer meshRenderer = item.GetComponent<MeshRenderer>();
            meshRenderer.material.DisableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", color);

        }
        countDownTime = 0;
        countDown = 0;
        goal.Close();
    }

    public void RayStay(Laser laser)
    {
        CountUp(Color.red);
    }

    private void CountUp(Color color)
    {
        if(countDown >= switchObj.Length)
        {
            countDownTime = intervalTime * 3;
            goal.Open();
            return;
        }

        countDownTime += Time.deltaTime;

        if (countDownTime > intervalTime * 3)
        {
            ObjectEmission(switchObj[3], color);
            countDown = 4;
        }
        else if (countDownTime > intervalTime * 2)
        {
            ObjectEmission(switchObj[2], color);
        }
        else if (countDownTime > intervalTime)
        {
            ObjectEmission(switchObj[1], color);
        }
        else if (countDownTime > 0)
        {
            ObjectEmission(switchObj[0], color);
        }
    }
    private void ObjectEmission(GameObject obj, Color color)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        meshRenderer.material.EnableKeyword("_EMISSION");

        float factor = Mathf.Pow(2, intensity);

        meshRenderer.material.SetColor("_EmissionColor", new Color(color.r * factor,color.g * factor,color.b * factor));
    }
}
