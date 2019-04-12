using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

    int keysToFind;
    int keysCollected;

    GameObject doorObject;
    LinkedList<string> allScenes = new LinkedList<string>();
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();


        allScenes.AddLast("Level_1");
        allScenes.AddLast("Level_2");
        allScenes.AddLast("Hardest");

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
  
        if (col.gameObject.tag == "Exit" && !isLocked) 
		{
            // Next Level / WIN!
            NextLevel();
        }
	}

    void NextLevel ()
    {

        if (currentScene.name != allScenes.Last.Value)
        {
            // Next Level              
            SceneManager.LoadScene(allScenes.Find(currentScene.name).Next.Value);
        }
        else
        {
            // Win!
            GotToMainMenu();
        }
    }

    void GotToMainMenu ()
    {
        // Might be good to add transition to this
        SceneManager.LoadScene("MainMenu");
    }

	void Update()
	{
		if (Input.GetKey ("escape")) 
		{
            GotToMainMenu();
		}
        if (Input.GetKey("r"))
        {
            RestartGame();
        }
        if (Input.GetKey("n"))
        {
            // could re-add && (Debug.isDebugBuild || Application.isEditor) to make this "cheaty"
            NextLevel();
        }
    }

	void RestartGame()
	{
        keysCollected = 0;
		SceneManager.LoadScene (currentScene.name);
        Time.timeScale = 1;
        
    }
}
