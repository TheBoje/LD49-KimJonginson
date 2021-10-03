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

    [SerializeField] private GameObject _rightShoulderPivot;
    [SerializeField] private GameObject _rightArm;
    [SerializeField] private GameObject _rightElbowPivot;
    [SerializeField] private GameObject _rightForearm;
    [SerializeField] private GameObject _rightHand;
    [SerializeField] private GameObject _rightHandPivot;

    [SerializeField] private GameObject _leftShoulderPivot;
    [SerializeField] private GameObject _leftArm;
    [SerializeField] private GameObject _leftHandPivot;

    private GameObject _rightElbowTarget;

    private GameObject _controllerTopPivot;

    private float _rightArmLength;
    private float _rightForearmLength;

    public GameObject debugPoint;

    private void snapRightHand()
    {
        // _rightHandPivot.transform.position = _controllerTopPivot.transform.position;

        Vector3 b_prime = (_rightShoulderPivot.transform.position + _controllerTopPivot.transform.position) / 2f;

        Vector3 newElbowPosition = Vector3.MoveTowards(b_prime, _rightElbowTarget.transform.position, Vector3.Distance(b_prime, _rightElbowTarget.transform.position) / 2f);
        
        debugPoint.transform.position = newElbowPosition;
        
        SnapBetween(_rightArm, newElbowPosition, _rightShoulderPivot.transform.position);
        
        SnapBetween(_rightForearm, _controllerTopPivot.transform.position, newElbowPosition);
    }

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
        _rightShoulderPivot = transform.Find("RightShoulderPivot").gameObject;
        _rightArm = transform.Find("RightArm").gameObject;
        _rightElbowPivot = transform.Find("RightElbowPivot").gameObject;
        _rightForearm = transform.Find("RightForearm").gameObject;
        _rightHand = _rightForearm.transform.Find("RightHand").gameObject;
        _rightHandPivot = _rightForearm.transform.Find("RightHandPivot").gameObject;

        _rightElbowTarget = transform.Find("RightElbowTarget").gameObject;
        
        _leftShoulderPivot = transform.Find("LeftShoulderPivot").gameObject;
        _leftArm = _leftShoulderPivot.transform.Find("LeftArm").gameObject;
        _leftHandPivot = _leftArm.transform.Find("LeftHandPivot").gameObject;

        _controllerTopPivot = stand.transform.Find("ControllerBottomPivot").Find("Controller").Find("ControllerTopPivot").gameObject;


    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        snapRightHand();
        // SnapBetween(_leftHand, _leftShoulderPivot, _buttonPivot);
        // SnapBetween(_rightHand, _rightShoulderPivot, _controllerPivot);
    }
    
}