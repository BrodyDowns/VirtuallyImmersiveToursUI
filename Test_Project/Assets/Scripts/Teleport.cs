using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
	public string sceneName = "";
	private Collider bubble;
    
    void Start() {

		//Set sceneName to currently active scene if empty.
		Scene currentScene = SceneManager.GetActiveScene();
		if (sceneName == "") {
			sceneName = currentScene.name;
		}
	}

	//Load the scene associated with sceneName if collision occurs. 
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadYourAsyncScene());  
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
		
		// Set new active scene to switch scenes.
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}