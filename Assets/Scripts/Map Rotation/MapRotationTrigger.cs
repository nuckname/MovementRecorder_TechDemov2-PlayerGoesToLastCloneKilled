using UnityEngine;

public class MapRotationTrigger : MonoBehaviour
{
    public Transform player; // The player object to keep in view during camera rotation
    public float rotationAngle = 90f; // Angle to rotate the camera by

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Reference to the main camera
    }

    void OnTriggerEnter(Collider other)
    {
        // When the player enters the trigger, rotate the camera
        if (other.CompareTag("Player"))
        {
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        // Rotate the camera around the player's position by 90 degrees
        mainCamera.transform.RotateAround(player.position, Vector3.up, rotationAngle);
    }
}