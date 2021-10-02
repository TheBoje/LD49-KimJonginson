using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    void print()
    {
        Debug.Log("This is a debug call method");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello, world!");
        Debug.Log("This is working properly");
        this.print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
