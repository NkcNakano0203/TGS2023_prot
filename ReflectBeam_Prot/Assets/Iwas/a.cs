using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    LineRenderer lineRenderer;

    Vector3[] re = new Vector3[10];

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        for (int i = 0; i < re.Length; ++i)
        {
            re[i] = new Vector3(i, 0, 0);
        }
        lineRenderer.positionCount = re.Length;
        lineRenderer.SetPositions(re);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
