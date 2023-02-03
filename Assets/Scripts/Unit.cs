using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private Animator unitAnimator;
    
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = .1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);

            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
