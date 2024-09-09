using System;
using UnityEngine;
using System.Collections.Generic;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 10f;
    private List<Vector3> positions;
    private float moveSpeed = 0f;
    private int indexNum = 0;

    private void Start()
    {
        Debug.Log("debug");
    }

    public void SetTrajectory(List<Vector3> trajectoryPoints)
    {
        positions = trajectoryPoints;
        if (positions.Count > 0)
        {
            transform.position = positions[0];
        }
    }

    private void Update()
    {
        if (positions == null || indexNum >= positions.Count - 1)
            return;

        // Calculate the distance between the current segment points
        float segmentDistance = Vector3.Distance(positions[indexNum], positions[indexNum + 1]);

        // Increase lerp value relative to the distance between points to keep the speed consistent
        moveSpeed += speed * Time.deltaTime / segmentDistance;

        // Round lerp value down to int
        indexNum = Mathf.FloorToInt(moveSpeed);

        // Ensure indexNum does not exceed the length of positions
        if (indexNum >= positions.Count - 1)
        {
            indexNum = positions.Count - 2; // Clamp to the last segment
        }

        // Lerp between current segment points
        transform.position = Vector3.Lerp(positions[indexNum], positions[indexNum + 1], moveSpeed - indexNum);
    }
}