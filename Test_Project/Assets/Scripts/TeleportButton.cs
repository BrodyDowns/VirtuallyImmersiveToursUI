using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportButton : MonoBehaviour {

    public string sceneName = "";
    public GameObject player;

    public void Teleport()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {

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
