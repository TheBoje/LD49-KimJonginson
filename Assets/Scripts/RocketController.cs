using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;

    public GameObject kim;

    private Vector3 _rotation;

    private TrailRenderer _trail;
    public float timestamp = 0.5f;

    private float _startTime;

    public float fuel = 50f;

    private void Start()
    {
        _trail = gameObject.GetComponentInChildren<TrailRenderer>();
        _trail.time = fuel;
        _startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var t = transform;
        t.localPosition += -t.forward * Time.deltaTime * speed;

        float xRot = t.rotation.x;
        transform.Rotate(Vector3.up * kim.GetComponent<PlayerController>().sideways * rotationSpeed);

        if (Time.time - _startTime > timestamp)
        {
            _startTime = Time.time;
            ToggleTrail();
        }   
    }

    void ToggleTrail()
    {
        _trail.emitting = !_trail.emitting;
    }
}
