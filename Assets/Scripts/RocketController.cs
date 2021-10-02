using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class RocketController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public Vector3 maxScale;
    
    public float minSpeed = 0f;
    public Vector3 minScale;

    private float _speed;

    public GameObject kim;

    private Vector3 _rotation;

    private TrailRenderer _trail;
    public float timestamp = 0.5f;

    private float _startTime;

    public float fuel = 50f;

    public float scaleMultiplier = 0.1f;
    public float speedMultiplier = 0.1f;

    private bool _isGoingToExplode;

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

        if (_speed > maxSpeed && _isGoingToExplode)
            _speed = Mathf.Lerp(_speed, minSpeed, Time.deltaTime * speedMultiplier);

        var t = transform;
        t.localPosition += -t.forward * Time.deltaTime * _speed;

        float xRot = t.rotation.x;
        //transform.Rotate(Vector3.up * kim. * rotationSpeed);

        if (Time.time - _startTime > timestamp)
        {
            _startTime = Time.time;
            ToggleTrail();
        }
    }

    void Explode()
    {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.main.duration);
    }

    void ToggleTrail()
    {
        _trail.emitting = !_trail.emitting;
    }
}
