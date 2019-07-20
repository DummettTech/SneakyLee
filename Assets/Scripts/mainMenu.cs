using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void StarGame ()
    {
        SceneManager.LoadScene("Level_1");
    }

    public static void ExitGame ()
    {
        Application.Quit();
    }

    public void OpenGithub ()
    {
        Application.OpenURL("https://github.com/DummettTech/SneakyLee");
    }
}
