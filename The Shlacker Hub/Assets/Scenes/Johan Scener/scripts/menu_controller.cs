using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_controller : MonoBehaviour {

   
    private Rigidbody denna;

    public void Start()
    {
        
    }

    public void Update()
    {
       
    }

    void OnCollisionEnter(Collision col)
    {  
        if (col.gameObject.tag == "Finish")
        {
            
        }
    }

    public void NextScene(string scene)
    {
        SceneManager.LoadScene("Johan_Helpfullscenegame1");
    }


}
