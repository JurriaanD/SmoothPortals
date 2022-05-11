using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectiblebeam : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + 100 * Vector3.up);
    }
}
