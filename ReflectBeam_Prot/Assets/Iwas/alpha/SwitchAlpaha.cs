using UnityEngine;

public class SwitchAlpaha : MonoBehaviour, IRayReceiverAlpha
{
    [SerializeField]
    Goal goal;

    public void RayEnter(Laser laser)
    {
        goal.Open();
    }

    public void RayExit(Laser laser)
    {
        goal.Close();
    }
}
