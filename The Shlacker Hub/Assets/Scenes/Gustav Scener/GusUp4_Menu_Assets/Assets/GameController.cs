using MyTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Invoke("EndGame", 2.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EndGame()
    {
        GameUtils.LoadScene(2);
    }
}
