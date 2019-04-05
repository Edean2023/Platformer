using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}