using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    // Reference to stand
    public GameObject stand;


    private GameObject _controllerTopPivot;


    public GameObject rightArm;
    public GameObject rightArmPivot;
    public GameObject forarmPivot;
    public GameObject forarm;
    public GameObject controller;
    

    private void SnapBetween(GameObject main, Vector3 from, Vector3 to)
    {
        Vector3 dir = to - from;
        Vector3 middle = dir / 2f + from;
        main.transform.position = middle;
        main.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        forarm.transform.LookAt(controller.transform);
        forarm.transform.position = controller.transform.position - Vector3.right * 0.4f ;
        
        rightArm.transform.LookAt(forarmPivot.transform);
        // rightArm.transform.LookAt(forarmPivot.transform);
        // SnapBetween(_leftHand, _leftShoulderPivot, _buttonPivot);
        // SnapBetween(_rightHand, _rightShoulderPivot, _controllerPivot);
    }
    
}