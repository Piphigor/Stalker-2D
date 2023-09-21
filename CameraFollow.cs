using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 25;
    [SerializeField] private float smooth = 0.2f;
    [SerializeField] private Transform target;
    private Vector2 _vel;
    private void Awake()
    {
        if (target == null) target = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        transform.position = target.position;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref _vel, smooth, maxSpeed);
    }
}
