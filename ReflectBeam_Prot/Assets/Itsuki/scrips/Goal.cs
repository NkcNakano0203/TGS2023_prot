using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool isOpen = false;
    BoxCollider boxCollider;
    [SerializeField]
    MeshRenderer meshRenderer;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        boxCollider.enabled = isOpen;
        meshRenderer.enabled = isOpen;
    }

    public void Open()
    {
        isOpen = true;
    }

    public void Close()
    {
        isOpen = false;
    }
}
