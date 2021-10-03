using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public GameObject rocket;
    public GameObject usa;
    public GameObject korea;
    private InputController _inputController;

    public ParticleSystem koreaExplosion;

    [SerializeField] private float _targetDistance;

    private PlayableDirector _timeline;
    private bool _isGameEnded;

    float ComputeTargetDistance()
    {
        return Vector3.Distance(rocket.transform.position, usa.transform.position);
    }

    /**
     * Will change the perlin scale depending on the distance between the target and the rocket
     */
    void ComputePerlinScale()
    {
        _inputController.PerlinScale = 1 / _targetDistance * 2;
    }
    
    IEnumerator LoadNextScene(String sceneName)
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(sceneName);
    }

    private void ComputeEndGame()
    {
        if (rocket.GetComponent<RocketController>().IsExploded)
        {
            _isGameEnded = true;
            if (usa.GetComponent<TargetController>().IsInTarget)
            {
                Debug.Log("WIN !!!");
                StartCoroutine(LoadNextScene("WinMenu"));
            }
            else if(korea.GetComponent<TargetController>().IsInTarget)
            {
                koreaExplosion.Play();
                Debug.Log("Boom boom dans la Corée");
                StartCoroutine(LoadNextScene("KoreaEndMenu"));
            }
            else
            {
                Debug.Log("LOSE !!!");
                StartCoroutine(LoadNextScene("LooseMenu"));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputController = GetComponent<InputController>();
        _targetDistance = ComputeTargetDistance();
        rocket.GetComponent<RocketController>().enabled = false;
        _timeline = GetComponent<PlayableDirector>();
        _isGameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        _targetDistance = ComputeTargetDistance();
        ComputePerlinScale();
        if(!_isGameEnded)
            ComputeEndGame();

        if (_timeline.state != PlayState.Playing && !rocket.GetComponent<RocketController>().enabled)
            rocket.GetComponent<RocketController>().enabled = true;
    }
}