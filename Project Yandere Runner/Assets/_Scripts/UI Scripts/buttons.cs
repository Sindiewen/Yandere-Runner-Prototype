using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{

    public void startGame()
    {
        SceneManager.LoadScene("Game_Test");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
