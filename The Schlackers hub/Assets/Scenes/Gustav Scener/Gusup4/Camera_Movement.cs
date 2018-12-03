using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField]
    float speedIsKey = 0.05f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.sceneCount + 1 == SceneManager.GetActiveScene().buildIndex)
            transform.position += Vector3.down * ((Mathf.Abs(GusUp4_DataStorage.ActionIndex - 2) * 0.2f) + 0.2f) * speedIsKey;

        else transform.position += Vector3.down * ((GusUp4_DataStorage.ActionIndex * 0.2f) + 0.1f) * speedIsKey;
	}
}
