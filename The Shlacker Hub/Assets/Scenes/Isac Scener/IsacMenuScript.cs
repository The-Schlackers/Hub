using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsacMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Back()
    {
        GameUtils.LoadScene(0);
    }

    public void Game1()
    {
        GameUtils.LoadScene(22);
    }
}
