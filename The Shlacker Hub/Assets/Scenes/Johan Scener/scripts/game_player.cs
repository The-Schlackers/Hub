using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_player : MonoBehaviour {

    Vector3 startpos;
    Rigidbody denna;
    bool dead = false;
    bool victory = false;
    

    public void Start()
    {
       
    }

    void OnCollisionEnter(Collision col)
        {           

        if (col.gameObject.tag == "Tank")
        {
            dead = true;
            
        }  
        
        if (col.gameObject.tag == "Finish")
        {
            victory = true;
            
        }
    }

    void Update()
    {
        if (dead == true)
        {
            //startpos = transform.position;

            //startpos.x += 5f;
            transform.position = new Vector3(4f, 4f, -25f);
            denna = GetComponent<Rigidbody>();
            //This locks the RigidBody so that it does not move or rotate in the Z axis.
            denna.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
            denna.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX;
            denna.constraints = RigidbodyConstraints.None;
            //transform.position = startpos;
            //startpos.transform(0, 0, 0);
            //GetComponent<this>.startpos(0, 0, 0);
            dead = false;
        }
       if (victory == true)
        {
            SceneManager.LoadScene("Main_Scene");
        }
    }
}
