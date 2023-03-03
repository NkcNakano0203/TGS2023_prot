using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool isOpen = false;
    CapsuleCollider capsuleCollider;
    [SerializeField]
    MeshRenderer meshRenderer;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        capsuleCollider.enabled = isOpen;
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
