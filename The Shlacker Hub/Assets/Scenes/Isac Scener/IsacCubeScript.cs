using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsacCubeScript : MonoBehaviour {


    public float speed;


	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Jump", .5f, 1.2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(0,speed,0);
    }

}
