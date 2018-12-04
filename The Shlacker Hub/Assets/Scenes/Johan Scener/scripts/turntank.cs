using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turntank : MonoBehaviour {

    //public Rigidbody test;

    void OnCollisionEnter(Collision col)
    {

        
        if (col.gameObject.tag == "Respawn")
        {
            //test = GetComponent<Rigidbody>();
            //if (gameObject == test)
            //{
               transform.Rotate(0, 180, 0);
            //}



        }
    }
}
