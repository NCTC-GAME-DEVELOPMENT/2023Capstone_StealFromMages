using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }

    public void LoadLevelOne()
    {
        Debug.Log("Load Level One");
        SceneManager.LoadScene("LevelOne");
    }

}
