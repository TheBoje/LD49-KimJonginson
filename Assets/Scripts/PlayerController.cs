using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    
    private GameObject _leftController;
    private GameObject _leftControllerPivot;
    private GameObject _rightController;
    private GameObject _rightControllerPivot;
    
    

    private void getInput()
    {
        
    }
    
    // Set left hand to middle in between leftShoulderPivot & leftControllerPivot
    private void SnapLeftHand()
    {
        Vector3 leftDirection = _leftControllerPivot.transform.position - _leftShoulderPivot.transform.position;
        Vector3 leftMiddle = leftDirection / 2f + _leftShoulderPivot.transform.position;
        _leftHand.transform.position = leftMiddle;
        _leftHand.transform.rotation = Quaternion.FromToRotation(Vector3.up, leftDirection);
    }

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
        // Set hand position to stand left and right controllers' position
        // Find controllers and controllers` pivots in scene
        _leftController = stand.transform.Find("LeftController").gameObject;
        _rightController = stand.transform.Find("RightController").gameObject;
        _leftControllerPivot = _leftController.transform.Find("LeftControllerPivot").gameObject;
        _rightControllerPivot = _rightController.transform.Find("RightControllerPivot").gameObject;
        // Find shoulder pivots
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
        SnapBetween(_leftHand, _leftShoulderPivot, _leftControllerPivot);
        SnapBetween(_rightHand, _rightShoulderPivot, _rightControllerPivot);
    }
}