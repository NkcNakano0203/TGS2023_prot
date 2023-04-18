// 作成日: 作成者:市瀬
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SwitchButtonMove : MonoBehaviour
{
    private Vector3 parentPos;
    [SerializeField]
    private float minY = 0.35f;
    [SerializeField]
    private float moveSpeed = 0.05f;
    [SerializeField]
    private bool active = false;

    private void Start()
    {
        parentPos = transform.parent.position;
    }

    private void Update()
    {
        if (active && transform.position.y > parentPos.y - minY)
        {
            transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
        }
        else
        {
            active = false;
        }

        if (!active && transform.position.y < parentPos.y)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }
}