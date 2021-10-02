using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public GameObject rocket;
    public GameObject usa;
    public GameObject korea;
    private InputController _inputController;

    [SerializeField] private float _targetDistance;


    float ComputeTargetDistance()
    {
        return Vector3.Distance(rocket.transform.position, usa.transform.position);
    }

    /**
     * Will change the perlin scale depending on the distance between the target and the rocket
     */
    void ComputePerlinScale()
    {
        _inputController.PerlinScale = 1 / _targetDistance;
    }

    private void ComputeEndGame()
    {
        if (rocket.GetComponent<RocketController>().IsExploded)
        {
            if (usa.GetComponent<TargetController>().IsInTarget)
                Debug.Log("WIN !!!");
            else if(korea.GetComponent<TargetController>().IsInTarget)
                Debug.Log("Boom boom dans la Corée");
            else
                Debug.Log("LOSE !!!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputController = GetComponent<InputController>();
        _targetDistance = ComputeTargetDistance();
    }

    // Update is called once per frame
    void Update()
    {
        _targetDistance = ComputeTargetDistance();
        ComputePerlinScale();
        ComputeEndGame();
    }
}