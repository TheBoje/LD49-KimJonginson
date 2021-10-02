using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rocket;
    public GameObject target;
    private InputController _inputController;

    [SerializeField] private float _targetDistance;


    float ComputeTargetDistance()
    {
        return Vector3.Distance(rocket.transform.position, target.transform.position);
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
            if (target.GetComponent<USAController>().IsInUsa)
                Debug.Log("WIN !!!");
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