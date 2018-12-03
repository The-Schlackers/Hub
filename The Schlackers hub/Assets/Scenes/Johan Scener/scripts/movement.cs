using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public Rigidbody player;
    //public float movespeed = 15f;
    //public float turnspeed = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
    void Update()
    {
        
        //player.AddForce(0, 0, force * Time.deltaTime);
        if (Input.GetKey("d"))
        {
            player.AddForce(800 * Time.deltaTime, 0f, 0f);
            //transform.Rotate(Vector3.up, -turnspeed * Time.deltaTime);
        }

        if (Input.GetKey("w"))
        {
            player.AddForce(0f, 0f, 800 * Time.deltaTime);
            //transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            player.AddForce(0f, 0f, -800 * Time.deltaTime);
            //transform.Translate(-Vector3.forward * movespeed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            player.AddForce(-800 * Time.deltaTime, 0f, 0f);
            //transform.Rotate(Vector3.up, turnspeed * Time.deltaTime);
        }        
    }
}