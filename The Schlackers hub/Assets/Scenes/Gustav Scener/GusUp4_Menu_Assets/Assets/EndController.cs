using MyTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Invoke("GoToMenu", 2.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void GoToMenu()
    {
        GameUtils.LoadScene(0);
    }
}
