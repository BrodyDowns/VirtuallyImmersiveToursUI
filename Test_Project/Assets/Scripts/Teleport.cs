using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
	public string sceneName = "";
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadYourAsyncScene());  
    }

    IEnumerator LoadYourAsyncScene() {

        DontDestroyOnLoad(player);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
		
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
}