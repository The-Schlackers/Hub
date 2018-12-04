using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DudeScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            GameUtils.LoadScene(4);
        }
        else if (other.tag == "Truck")
        {
            GameUtils.LoadScene(2);
        }
        else if (other.tag == "Brunn")
        {
            GameUtils.LoadScene(3);
        }
        else if (other.tag == "Eld")
        {
            GameUtils.LoadScene(5);
        }
        else if (other.tag == "Trappa")
        {
            GameUtils.LoadScene(6);
        }
    }
    

}
