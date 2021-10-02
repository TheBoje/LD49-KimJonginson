using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class RocketController : MonoBehaviour
{
    private const int NB_BARS_FUEL = 20;
    
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

    public float maxFuel = 50f;
    private float _fuel;

    public float scaleMultiplier = 0.1f;
    public float speedMultiplier = 0.1f;
    public float consumerMultiplier = 0.1f;

    private bool _isGoingToExplode;
    private bool _isExploded;

    public float rotationSpeed = 0.1f;

    public ParticleSystem particleSystem;

    public TextMesh fuelLevel;
    
    private int ComputeFuelLeft()
    {
        return (int) Mathf.Floor((_fuel * NB_BARS_FUEL) / maxFuel);
    }

    private void PrintFuel()
    {
        String str = "";
        int nbBars = ComputeFuelLeft();

        for (int i = 0; i < nbBars; i++)
        {
            str += '|';
        }

        for (int i = nbBars; i < NB_BARS_FUEL; i++)
        {
            str += '.';
        }

        fuelLevel.text = "Fuel " + str;
    }

    private void ConsumeFuel()
    {
        _fuel = Mathf.Lerp(_fuel, 0, Time.deltaTime * consumerMultiplier);

        if (_fuel <= 2)
            _isGoingToExplode = true;
    }
    
    private void Start()
    {
        _speed = minSpeed;
        transform.localScale = minScale;
        _trail = gameObject.GetComponentInChildren<TrailRenderer>();
        _trail.time = maxFuel;
        _startTime = Time.time;
        _isGoingToExplode = false;
        _fuel = maxFuel;
        _isExploded = false;
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
        
        if(Math.Abs(_speed - minSpeed) < 0.1f && _isGoingToExplode && !_isExploded)
        {
            _speed = 0;
            Explode();
        }

        var t = transform;
        t.localPosition += -t.forward * Time.deltaTime * _speed;

        float xRot = t.rotation.x;
        transform.Rotate(Vector3.up * gm.GetComponent<InputController>().sideways * rotationSpeed);

        if (gm.GetComponent<InputController>().explode && !_isGoingToExplode)
            _isGoingToExplode = true;

        if (Time.time - _startTime > timestamp)
        {
            _startTime = Time.time;
            ToggleTrail();
        }
        ConsumeFuel();
        PrintFuel();
    }
    
    void Explode()
    {
        particleSystem.Play();
        Destroy(particleSystem, particleSystem.main.duration);
        _isExploded = true;
    }

    void ToggleTrail()
    {
        _trail.emitting = !_trail.emitting;
    }

    public bool IsExploded => _isExploded;
}
