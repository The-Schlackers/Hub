using MyTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartGame()
    {
        Debug.Log("Starting Game");

        GameUtils.LoadScene(1);
    }
}

//GameUtils utils = new GameUtils