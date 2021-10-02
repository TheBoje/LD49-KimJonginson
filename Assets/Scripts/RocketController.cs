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

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition += -transform.forward * Time.deltaTime * speed;

        float xRot = transform.rotation.x;
        Debug.Log(kim.GetComponent<PlayerController>().sideways);
        transform.Rotate(Vector3.up * kim.GetComponent<PlayerController>().sideways * rotationSpeed);
    }
}
