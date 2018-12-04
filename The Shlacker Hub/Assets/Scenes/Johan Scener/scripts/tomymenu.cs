using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tomymenu : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Finish")
        {

            SceneManager.LoadScene(3);

        }

        
    }
}
