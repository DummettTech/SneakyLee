using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

    int keysToFind;
    int keysCollected;

    GameObject doorObject;

    void Start()
    {
        doorObject = GameObject.FindGameObjectWithTag("Exit");

        keysToFind = GameObject.FindGameObjectsWithTag("Key").Length;
        if(keysToFind != 0)
        {
            lockDoor();
        }
    }

    bool isLocked = false;
    void lockDoor()
    {
        isLocked = true;
        doorObject.GetComponentInChildren<Light>().color = Color.red;
    }

    void unlockDoor()
    {
        isLocked = false;
        doorObject.GetComponentInChildren<Light>().color = Color.green;
        doorObject.GetComponent<Animator>().SetBool("IsOpen", true); 
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Key")
        {
            Destroy(col.gameObject);
            keysCollected = keysCollected + 1;
            Debug.Log("Keys to Find:" + keysToFind + "  Keys Found:" + keysCollected);
            if (keysCollected == keysToFind)
            {
                unlockDoor();
            }
        }
    }

    void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.tag == "Guard") 
		{
			// Game Over
			RestartGame();
		}

        string[] levelNames = { "level_1", "level_2", "level_3" };
        int currentLevel = 0;
        if (col.gameObject.tag == "Exit" && !isLocked) 
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
        keysCollected = 0;
        Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (currentScene.name);
        Time.timeScale = 1;
        
    }
}
