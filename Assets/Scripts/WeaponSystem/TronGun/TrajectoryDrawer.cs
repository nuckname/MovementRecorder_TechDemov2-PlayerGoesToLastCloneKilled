using UnityEngine;
using System.Collections.Generic;

public class TrajectoryDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int maxBounceCount = 5;
    public float maxDistance = 100f;
    public float velocity = 10f;
    public LayerMask collisionMask;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private List<Vector3> points;
    
    private bool isDrawing = true;
    private float fadeTime = 4;
    private float fadeDuration = 2;

    private void Update()
    {
        DrawTrajectory();
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            FireProjectile();
        }
        
        //not working
        /*
        if (isDrawing && fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            lineRenderer.startColor = Color.Lerp(lineRenderer.startColor, Color.blue, fadeTime / fadeDuration);
            lineRenderer.endColor = Color.Lerp(lineRenderer.endColor, Color.blue, fadeTime / fadeDuration);
        }
        */
        
    }

    private void DrawTrajectory()
    {
        points = new List<Vector3>();
        Vector3 startPosition = transform.position;
        //this is forward idk why.
        Vector3 direction = -transform.right;
        Vector3 startVelocity = direction * velocity;

        points.Add(startPosition);

        RaycastHit hit;
        Vector3 currentPosition = startPosition;
        Vector3 currentVelocity = startVelocity;

        for (int i = 0; i < maxBounceCount; i++)
        {
            if (Physics.Raycast(currentPosition, currentVelocity, out hit, maxDistance, collisionMask))
            {
                currentPosition = hit.point;
                currentVelocity = Vector3.Reflect(currentVelocity, hit.normal);
                points.Add(currentPosition);

                // Optionally reduce the velocity after each bounce
                // currentVelocity *= 0.9f; // Example: reduce by 10%
            }
            else
            {
                points.Add(currentPosition + currentVelocity.normalized * maxDistance);
                break;
            }
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public List<Vector3> GetTrajectoryPoints()
    {
        return points;
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        ProjectileMover mover = projectile.GetComponent<ProjectileMover>();
        if (mover != null)
        {
            mover.SetTrajectory(GetTrajectoryPoints());
        }
    }
}