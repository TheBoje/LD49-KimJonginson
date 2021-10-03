using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("Quitting app!");
        Application.Quit();
    }

    public void goToOptions()
    {
        Debug.Log("Going to options - TODO");
    }

    public void goToAbout()
    {
        Debug.Log("Going to about - TODO");
    }
}
