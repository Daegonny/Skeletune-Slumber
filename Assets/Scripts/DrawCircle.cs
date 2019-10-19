using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public int vertexCount = 40;
    public float lineWidth = 0.2f;
    public float radius;
    public bool circleFillScreen;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        setupCircle();
    }

    private void setupCircle()
    {
        lr.widthMultiplier = lineWidth;


        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        lr.positionCount = vertexCount;
        for (int i = 0; i < lr.positionCount; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lr.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / 40f;
        float theta = 0f;

        Vector3 oldPos = Vector3.zero;
        for(int i =0; i<vertexCount +1; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldPos, transform.position + pos);
            oldPos = transform.position + pos;
            theta += deltaTheta;
        }
    }
#endif
}
