﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform rocketTransform;
    [SerializeField] private float smoothSpeed = 0.125f;
    
    [SerializeField] private Vector3 offset;
        
        
    private void FixedUpdate()
    {
        Vector3 desiredPosition = rocketTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        transform.LookAt(rocketTransform);
    }
}
