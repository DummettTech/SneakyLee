using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
        
		if (col.gameObject.tag == "Guard") 
		{
			// Game Over
			RestartGame();
		}
        Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Exit") 
		{
            // Next Level / WIN!
            Debug.Log("You Win!");
            Text gameCompleteText = GameObject.FindGameObjectWithTag("CompleteText").GetComponent<Text>();
            gameCompleteText.color = Color.yellow;
            Debug.Log(gameCompleteText.text);
            Time.timeScale = 0;
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
