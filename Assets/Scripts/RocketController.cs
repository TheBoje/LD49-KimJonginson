using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class RocketController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public Vector3 maxScale;
    
    public float minSpeed = 0f;
    public Vector3 minScale;

    private float _speed;

    public GameObject gm;

    private Vector3 _rotation;

    private TrailRenderer _trail;
    public float timestamp = 0.5f;

    private float _startTime;

    public float fuel = 50f;

    public float scaleMultiplier = 0.1f;
    public float speedMultiplier = 0.1f;

    private bool _isGoingToExplode;

    public float rotationSpeed = 0.1f;

    public ParticleSystem particleSystem;
    
    private void Start()
    {
        _speed = minSpeed;
        transform.localScale = minScale;
        _trail = gameObject.GetComponentInChildren<TrailRenderer>();
        _trail.time = fuel;
        _startTime = Time.time;
        _isGoingToExplode = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_speed < maxSpeed && !_isGoingToExplode)
            _speed = Mathf.Lerp(_speed, maxSpeed, Time.deltaTime * speedMultiplier);
        
        if (transform.localScale.x < maxScale.x && !_isGoingToExplode)
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime * scaleMultiplier);
       
        if(transform.localScale.x > minScale.x && _isGoingToExplode)
            transform.localScale = Vector3.Lerp(transform.localScale, minScale, Time.deltaTime * scaleMultiplier);

        if (_speed > minSpeed && _isGoingToExplode)
            _speed = Mathf.Lerp(_speed, minSpeed, Time.deltaTime * speedMultiplier);
        
        if(Math.Abs(_speed - minSpeed) < 0.1f && _isGoingToExplode)
            Explode();

        var t = transform;
        t.localPosition += -t.forward * Time.deltaTime * _speed;

        float xRot = t.rotation.x;
        transform.Rotate(Vector3.up * gm.GetComponent<InputController>().sideways * rotationSpeed);

        _isGoingToExplode = gm.GetComponent<InputController>().explode;

        if (Time.time - _startTime > timestamp)
        {
            _startTime = Time.time;
            ToggleTrail();
        }
    }

    void Explode()
    {
        particleSystem.Play();
        Destroy(gameObject, particleSystem.main.duration);
    }

    void ToggleTrail()
    {
        _trail.emitting = !_trail.emitting;
    }
}
