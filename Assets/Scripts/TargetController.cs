using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private bool _isInTarget;

    private void Start()
    {
        _isInTarget = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            _isInTarget = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            _isInTarget = false;
        }
    }

    public bool IsInTarget => _isInTarget;
}
