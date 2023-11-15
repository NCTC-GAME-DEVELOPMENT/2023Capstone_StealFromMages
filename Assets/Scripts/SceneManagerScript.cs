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

    public void LoadTitleScreen()
    {
        Debug.Log("Load TItle Screen");
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadEndScreen()
    {
        Debug.Log("Load End Screen");
        SceneManager.LoadScene("EndScreen");
    }

    public void LoadLevelOne()
    {
        Debug.Log("Load Level One");
        SceneManager.LoadScene("LevelOne");
    }

    public void LoadLevelTwo()
    {
        Debug.Log("Load Level Two");
        SceneManager.LoadScene("LevelTwo");
    }

    public void LoadLevelThree()
    {
        Debug.Log("Load Level Three");
        SceneManager.LoadScene("LevelThree");
    }
}
