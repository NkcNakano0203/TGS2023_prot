// 作成日: 作成者:市瀬
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SwitchButtonMove : MonoBehaviour
{
    [SerializeField]
    private float minZ = 0.35f;
    [SerializeField]
    private float maxZ = 0.5f;
    [SerializeField]
    private float moveSpeed = 0.05f;
    [SerializeField]
    private bool active = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(active && transform.position.z < minZ)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            active = false;
        }

        if (!active && transform.position.z < maxZ)
        {
            transform.position -= Vector3.forward * moveSpeed * Time.deltaTime;
        }
    }
}
