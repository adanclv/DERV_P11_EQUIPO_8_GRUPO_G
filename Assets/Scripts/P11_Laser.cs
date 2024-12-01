using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P11_Laser : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();            
    }
    
    public void SetPosition(Vector3 origin, Vector3 direction)
    {
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, direction);
    }

    public void SetActive(bool active)
    {
        lineRenderer.enabled = active;
    }
}
