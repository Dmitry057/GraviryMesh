using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private MKS _mks;
    private float t = 0f;
    private void Start()
    {
        _mks = GetComponent<MKS>();
        _lineRenderer = FindObjectOfType<LineRenderer>();
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (_mks.isRunning)
        {

            if (t >= 0.01)
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, transform.position);
                t = 0f;
            }
            else
            {
                t += Time.deltaTime;
            }
        }
    }
}

