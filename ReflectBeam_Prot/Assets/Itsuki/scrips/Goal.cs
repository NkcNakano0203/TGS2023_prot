using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool isOpen = false;
    CapsuleCollider capsuleCollider;
    MeshRenderer meshRenderer;

    [SerializeField]
    Material OpenMat;
    [SerializeField]
    Material closeMat;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        capsuleCollider.enabled = isOpen;
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
