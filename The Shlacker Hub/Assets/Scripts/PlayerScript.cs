using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float Speed = 0;
    [SerializeField]
    private float RotateSpeed = 0;

    [SerializeField]
    private Animator Ani;

    [SerializeField]
    private GameObject Man;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (PlayerPrefs.GetInt("LastLoadedScene") < 2 || PlayerPrefs.GetInt("LastLoadedScene") > 6)
            Instantiate(Man, new Vector3(0, 40, 0), Quaternion.Euler(new Vector3(-71.911f, 0f, 0f)));

        //Portal Eject
        else if (PlayerPrefs.GetInt("LastLoadedScene") == 4) 
        {
            
            GameObject go = Instantiate(Man, new Vector3(1.0f, 1.49f, 14.24f), Quaternion.Euler(new Vector3(-25.892f, 0f, 0f)));

            GameObject go1 = go.transform.Find("Torso").gameObject;

            if (go1 != null)
            {
                go1.GetComponent<Rigidbody>().AddForce(0, 10000, -10000);
            }


        }
        //Trash Eject
        else if (PlayerPrefs.GetInt("LastLoadedScene") == 2)
        {
            
            GameObject go = Instantiate(Man, new Vector3(16.05f, 3.03f, 2.0338f), Quaternion.Euler(new Vector3(57.568f, -90f, 0f)));

            GameObject go1 = go.transform.Find("Torso").gameObject;

            if (go1 != null)
            {
                go1.GetComponent<Rigidbody>().AddForce(-10000, 10000, 0);
            }


        }
        //Brunn Eject
        else if (PlayerPrefs.GetInt("LastLoadedScene") == 3)
        {
            
            GameObject go = Instantiate(Man, new Vector3(11.1f, 3.9f, -14.21f), Quaternion.Euler(new Vector3(33.515f, -33.079f, 0f)));

            GameObject go1 = go.transform.Find("Torso").gameObject;

            if (go1 != null)
            {
                go1.GetComponent<Rigidbody>().AddForce(-4000, 10000, 6000);
            }


        }
        //Eld Eject
        else if (PlayerPrefs.GetInt("LastLoadedScene") == 5)
        {
            
            GameObject go = Instantiate(Man, new Vector3(-9.83f, 10.63f, -15.17f), Quaternion.Euler(new Vector3(23.066f, 18.519f, 0f)));

            GameObject go1 = go.transform.Find("Torso").gameObject;

            if (go1 != null)
            {
                go1.GetComponent<Rigidbody>().AddForce(3000, 10000, 7000);
            }


        }
        //Trappa eject
        else if (PlayerPrefs.GetInt("LastLoadedScene") == 6)
        {
            
            GameObject go = Instantiate(Man, new Vector3(-18.91f, 3.22f, 2.29f), Quaternion.Euler(new Vector3(32.582f, 90f, 0f)));

            GameObject go1 = go.transform.Find("Torso").gameObject;

            if (go1 != null)
            {
                go1.GetComponent<Rigidbody>().AddForce(10000, 10000, 0);
            }


        }

        PlayerPrefs.SetInt("LastLoadedScene", 0);
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {

        ScoopMove();

        if(Input.GetMouseButton(0))
        {
            Ani.SetBool("ButtonScoop", true);
        }
        else Ani.SetBool("ButtonScoop", false);
    }

    private void ScoopMove()
    {
        //Temp Inputs for horizontal and vertical movement
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");


        Vector3 pos = transform.position;
        pos += transform.forward * Speed * axisV * Time.deltaTime;
        pos += transform.right * Speed * axisH * Time.deltaTime;
        rb.MovePosition(pos);

        if (Input.GetKey(KeyCode.E))
        {
            rb.transform.Rotate(transform.up, RotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rb.transform.Rotate(transform.up, -RotateSpeed * Time.deltaTime);
        }



    }

    private void Scoop()
    {

        ////Check Left mouse 
        //if (Input.GetMouseButton(0))
        //{
        //    //Lerp between current pos to 0.43y
        //    rb.MovePosition(Vector3.Lerp(rb.position, new Vector3(rb.position.x, 0.43f, rb.position.z), 7f * Time.deltaTime));

        //    //Lerp between current rot to -11.853 Degrees z Axis


        //    rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, -11.853f), 3.5f * Time.deltaTime));
        //}
        //else
        //{
        //    if (rb.position.y != 2.43f)
        //    {
        //        //Return y pos and z rotation to "standard" set values

        //        rb.MovePosition(Vector3.Lerp(rb.position, new Vector3(rb.position.x, 2.43f, rb.position.z), 7f * Time.deltaTime));

        //        rb.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 4f), 7f * Time.deltaTime));

        //    }
        //}


        ////rb.MovePosition;
        ////rb.MoveRotation;
    }



}
