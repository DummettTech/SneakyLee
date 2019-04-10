using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
        string[] levelNames = {"level_1", "level_2" };
        int currentLevel = 0;

		if (col.gameObject.tag == "Guard") 
		{
			// Game Over
			RestartGame();
		}

		if (col.gameObject.tag == "Exit") 
		{
            // Next Level / WIN!
            currentLevel = currentLevel + 1;

            if (currentLevel != levelNames.Length)
            {
                SceneManager.LoadScene(levelNames[currentLevel]);
            }

            // Text gameCompleteText = GameObject.FindGameObjectWithTag("CompleteText").GetComponent<Text>();
            // gameCompleteText.color = Color.yellow;
            // Debug.Log(gameCompleteText.text);
            // Time.timeScale = 0;
        }
	}

	void Update()
	{
		if (Input.GetKey ("escape")) 
		{
			Application.Quit ();
		}
        if (Input.GetKey("r"))
        {
            RestartGame();
        }
    }

	void RestartGame()
	{
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.name);
        Time.timeScale = 1;
    }
}
