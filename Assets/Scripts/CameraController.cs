using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MinFollowYOffset = 2.0f;
    private const float MaxFollowYOffset = 13.0f;
    
    [SerializeField] private float cameraMoveSpeed = 5.0f;
    [SerializeField] private float cameraRotationSpeed = 100.0f;
    [SerializeField] private float zoomAmount = 1.0f;
    [SerializeField] private float zoomSpeed = 5.0f;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -1f;
        }

        Vector3 moveVector = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        
        transform.position += moveVector * (cameraMoveSpeed * Time.deltaTime);
    }
    
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * (cameraRotationSpeed * Time.deltaTime);
    }
    
    private void HandleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MinFollowYOffset, MaxFollowYOffset);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset,
            Time.deltaTime * zoomSpeed);
    }
}
