using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void starGame ()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void exitGame ()
    {
        Application.Quit();
    }

    public void openGithub ()
    {
        Application.OpenURL("https://github.com/DummettTech/SneakyLee");
    }
}
