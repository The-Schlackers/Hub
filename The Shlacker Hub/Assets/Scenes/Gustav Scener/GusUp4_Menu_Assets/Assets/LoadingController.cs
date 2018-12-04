using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LoadAsyncScene());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(2.0f);

        if (PlayerPrefs.HasKey("sceneToLoad") == false)
            yield break;

        int sceneIndexToLoad = PlayerPrefs.GetInt("sceneToLoad");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndexToLoad);

        while(operation.isDone == false)
        {
            yield return null;
        }
    }
}
