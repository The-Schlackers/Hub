using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
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

        if (PlayerPrefs.HasKey("SceneToLoad") == false)
            yield break; // ha detta så tidigt som möjligt // Sealisera stora saker ist för playerprefs

        AsyncOperation operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("SceneToLoad"));
        
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
