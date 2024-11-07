using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledPlayback : MonoBehaviour
{
    public static float playbackSpeed = 0.01f;

    private Vector3 _lastMousePosition;
    private float _mouseSpeed;
    private float _playerSpeed;

    public float minPlaybackSpeed = 0.01f;
    public float maxPlaybackSpeed;

    public Rigidbody playerRigidbody;

    private void Start()
    {
        _lastMousePosition = Input.mousePosition;
        playbackSpeed = minPlaybackSpeed;
    }

    private void Update()
    {
        CalculateMouseSpeed();
        CalculatePlayerMovementSpeed();
        AdjustPlaybackSpeed();
    }

    private void CalculateMouseSpeed()
    {
        Vector3 mouseDelta = Input.mousePosition - _lastMousePosition;
        
        _mouseSpeed = mouseDelta.magnitude / Time.deltaTime;
        
        _lastMousePosition = Input.mousePosition;
    }

    private void CalculatePlayerMovementSpeed()
    {
        _playerSpeed = playerRigidbody.velocity.magnitude;
    }

    private void AdjustPlaybackSpeed()
    {
        float combinedSpeed = _mouseSpeed + _playerSpeed;

        playbackSpeed = Mathf.Lerp(minPlaybackSpeed, maxPlaybackSpeed, Mathf.Clamp01(combinedSpeed / 100f));
    }
}