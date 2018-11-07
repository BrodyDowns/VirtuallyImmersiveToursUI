using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Teleports user to a scene, Meant to be used as a Button component
 * */
public class TeleportButton : MonoBehaviour {

    public string sceneName = "";
    public GameObject player;


    //Function to be called by Button
    public void Teleport()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        //Keep player game object
        DontDestroyOnLoad(player);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        //Destroys another game object with the tag 'Player'
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1) {
            Destroy(GameObject.Find(player.name));
        }

    }
}
