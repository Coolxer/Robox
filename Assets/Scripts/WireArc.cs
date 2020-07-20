using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireArc : MonoBehaviour
{
    public GameObject center;
    public Vector3 vector;

    public float startAngle;
    public float endAngle;
    public float radius;
    public int segments;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount =  segments;
        line.useWorldSpace = false;

        line.startWidth = line.endWidth = 20.0f;

        line.transform.position = center.transform.position;

        calculatePoints();
    }

    void calculatePoints()
    {
        float angle = startAngle;
        float arcLength = endAngle - startAngle;

        for (int i = 0; i < segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            if(vector == Vector3.right)
                line.SetPosition (i,new Vector3(x , y, 0));
            else if(vector == Vector3.up)
                line.SetPosition (i,new Vector3(x, 0, y));

            angle += (arcLength / segments);
        }
    }
}
