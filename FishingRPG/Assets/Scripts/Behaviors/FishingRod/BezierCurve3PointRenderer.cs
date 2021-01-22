using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve3PointRenderer : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;

    public LineRenderer lineRenderer;
    public int vertexCount = 12;

    private void Update()
    {
        var pointList = new List<Vector3>();
        for(float ratio = 0; ratio <= 1; ratio += 1.0f/vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(p1.position, p2.position, ratio);
            var tangentLineVertex2 = Vector3.Lerp(p2.position, p3.position, ratio);
            var tangentLineVertex3 = Vector3.Lerp(p3.position, p4.position, ratio);

            var bezierPoint1 = Vector3.Lerp(tangentLineVertex1, tangentLineVertex3, ratio);

            pointList.Add(bezierPoint1);
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.green;
        Gizmos.DrawLine(p1.position, p2.position);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(p2.position, p3.position);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(p3.position, p4.position);

        for(float ratio = 0.5f/vertexCount; ratio < 1; ratio += 1.0f/vertexCount)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector3.Lerp(p1.position, p2.position, ratio), Vector3.Lerp(p2.position, p3.position, ratio));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.Lerp(p2.position, p3.position, ratio), Vector3.Lerp(p3.position, p4.position, ratio));
        
        }
        */
    }
}
