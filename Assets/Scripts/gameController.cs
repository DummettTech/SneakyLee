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

    private void Start ()
    {
        currentScene = SceneManager.GetActiveScene();

        // TODO: replace manual list with itterating through addedScenes with name substring "Level" / buildOrder != 0
        allScenes.AddLast("Level_1");
        allScenes.AddLast("Level_3");
        allScenes.AddLast("Level_2");
        allScenes.AddLast("Level_4");
        allScenes.AddLast("Level_5");
        allScenes.AddLast("Hardest");

        doorObject = GameObject.FindGameObjectWithTag("Exit");
        keysToFind = GameObject.FindGameObjectsWithTag("Key").Length;

        if(keysToFind != 0)
        {
            // this allows a level to start with no keys 
            LockDoor();
        }
    }

    bool isLocked = false;

    private void LockDoor ()
    {
        isLocked = true;
        doorObject.GetComponentInChildren<Light>().color = Color.red;
    }

    private void UnlockDoor ()
    {
        isLocked = false;
        doorObject.GetComponentInChildren<Light>().color = Color.green;
        doorObject.GetComponent<Animator>().SetBool("IsOpen", true); 
    }

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Key")
        {
            Destroy(col.gameObject);
            keysCollected = keysCollected + 1;
            Debug.Log("Keys to Find:" + keysToFind + "  Keys Found:" + keysCollected);
            if (keysCollected == keysToFind)
            {
                UnlockDoor();
            }
        }
    }

    private void OnCollisionEnter (Collision col)
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

    private void NextLevel ()
    {

        if (currentScene.name != allScenes.Last.Value)
        {
            // Next Level              
            SceneManager.LoadScene(allScenes.Find(currentScene.name).Next.Value);
        }
        else
        {
            // Win!
            // TODO: Add intergration with the congratz HUD I've got hidden in all scenes
            GotToMainMenu();
        }
    }

    private void GotToMainMenu()
    {
        // Might be good to add transition to this
        SceneManager.LoadScene("MainMenu");
    }

    private void Update ()
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

    private void RestartGame ()
	{
        keysCollected = 0;
		SceneManager.LoadScene (currentScene.name);
        Time.timeScale = 1;
        
    }
}
