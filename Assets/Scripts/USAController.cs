using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class USAController : MonoBehaviour
{
    private bool _isInUSA;

    private void Start()
    {
        _isInUSA = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            Debug.Log("ENTER USA");
            _isInUSA = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            Debug.Log("EXIT USA");
            _isInUSA = false;
        }
    }

    public bool IsInUsa => _isInUSA;
}
