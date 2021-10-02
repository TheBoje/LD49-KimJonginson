using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class InputController : MonoBehaviour
{
    private PlayerActionControls _playerActionControls;


    public GameObject player;
    public GameObject stand;

    private GameObject _controllerBottomPivot;
    private Vector3 _controllerInitialRotation;  // This is final but final doesnt exist in c# so fk it

    [SerializeField] private float _perlinScale = 0.01f;
    [SerializeField] private float _xScale = 4.0f;
    
    private float _inputSideways;
    [SerializeField] private float lerpCoef = 0.4f;

    public float sideways;
    public bool explode = false;
    
    public void handleSidewaysInput(InputAction.CallbackContext context)
    {
        _inputSideways = _playerActionControls.Player.Controls.ReadValue<float>();
    }

    public void handleExplodeInput(InputAction.CallbackContext context)
    {
        explode = true;
    }

    private float getPerlinVariation()
    {
        return ((2 *  _perlinScale) * Mathf.PerlinNoise(Time.time * _xScale, 0.0f)) - _perlinScale;
    }
    
    private void LocalUpdateInput()
    {
        float temp = Mathf.Lerp(sideways, _inputSideways, lerpCoef * Time.deltaTime * 10);
        // Set input in [-1, 1] and suppress low float values
        if (temp >= 1)
        {
            sideways = 1f;
        } else if (temp <= -1)
        {
            sideways = -1f;
        } else if (temp < 0.01f && temp > -0.01f)
        {
            sideways = 0f;
        } else
        {
            sideways = temp;
        }
    }

    private void rotateStandControllers()
    {
        Vector3 temp = _controllerInitialRotation + new Vector3(sideways * 60, 0f, 0f); 
        _controllerBottomPivot.transform.rotation = Quaternion.Euler(temp);
    }
    
    // Start is called before the first frame update
    void Start()
    {   
        // Init controller references
        // Note(Louis): Please forget about raw string laying around they are mandatory
        _controllerBottomPivot = stand.transform.Find("ControllerBottomPivot").gameObject;
        _controllerInitialRotation = _controllerBottomPivot.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        LocalUpdateInput();
        sideways += getPerlinVariation();
        rotateStandControllers();
    }
    
    private void Awake()
    {
        _playerActionControls = new PlayerActionControls();
    }

    private void OnEnable()
    {
        _playerActionControls.Enable();
    }

    private void OnDisable()
    {
        _playerActionControls.Disable();
    }
}
