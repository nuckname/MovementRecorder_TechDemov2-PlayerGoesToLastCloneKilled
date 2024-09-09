using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TronBullet : MonoBehaviour
{
    public float speed = 10f;
    private List<Vector3> points;
    private int currentPointIndex = 0;

    public void SetTrajectory(List<Vector3> trajectoryPoints)
    {
        points = trajectoryPoints;
        if (points.Count > 0)
        {
            transform.position = points[0];
        }
    }

    private void Update()
    {
        if (points == null || currentPointIndex >= points.Count - 1)
            return;

        Vector3 targetPoint = points[currentPointIndex + 1];
        Vector3 moveDirection = (targetPoint - transform.position).normalized;
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPoint) <= step)
        {
            transform.position = targetPoint;
            currentPointIndex++;
        }
        else
        {
            transform.position += moveDirection * step;
        }
    }
}
