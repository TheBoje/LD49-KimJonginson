using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    // Reference to stand
    public GameObject stand;

    private GameObject _leftShoulderPivot;
    private GameObject _rightShoulderPivot;
    private GameObject _leftHand;
    private GameObject _leftHandPivot;
    private GameObject _rightHand;
    private GameObject _rightHandPivot;
    
    private GameObject _button;
    private GameObject _buttonPivot;
    private GameObject _controller;
    private GameObject _controllerPivot;

    private void SnapBetween(GameObject main, GameObject from, GameObject to)
    {
        Vector3 dir = to.transform.position - from.transform.position;
        Vector3 middle = dir / 2f + from.transform.position;
        main.transform.position = middle;
        main.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Find controllers and controllers` pivots in scene
        _controller = stand.transform.Find("ControllerBottomPivot").Find("Controller").gameObject;
        _controllerPivot = _controller.transform.Find("ControllerTopPivot").gameObject;
        _button = stand.transform.Find("Button").gameObject;
        _buttonPivot = _button.transform.Find("ButtonTopPivot").gameObject;
        // Find shoulder pivots in scene
        _leftShoulderPivot = transform.Find("LeftShoulderPivot").gameObject;
        _rightShoulderPivot = transform.Find("RightShoulderPivot").gameObject;
        // Find hands and hands` pivots in scene
        _leftHand = _leftShoulderPivot.transform.Find("LeftHand").gameObject;
        _rightHand = _rightShoulderPivot.transform.Find("RightHand").gameObject;
        _leftHandPivot = _leftHand.transform.Find("LeftHandPivot").gameObject;
        _rightHandPivot = _rightHand.transform.Find("RightHandPivot").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SnapBetween(_leftHand, _leftShoulderPivot, _buttonPivot);
        SnapBetween(_rightHand, _rightShoulderPivot, _controllerPivot);
    }
    
}