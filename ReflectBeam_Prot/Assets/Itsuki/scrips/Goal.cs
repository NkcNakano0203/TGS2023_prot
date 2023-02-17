using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool isOpen = false;
    BoxCollider boxCollider;
    MeshRenderer meshRenderer;

    [SerializeField]
    Material OpenMat;
    [SerializeField]
    Material closeMat;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        boxCollider.enabled = isOpen;
        meshRenderer.material = isOpen ? OpenMat : closeMat;
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
