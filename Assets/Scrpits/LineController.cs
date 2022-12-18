using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 point;
    private int pointCount = 0;
    bool starhit = false;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Vector3 point)
    {
        this.point = point;
    }

    public void SetLineOnStar(Vector3 point)
    {
        this.point = point;
        line.SetPosition(pointCount, point);
        pointCount++;
    }

    private void Update()
    {
            line.SetPosition(pointCount, point);  
    }
}
