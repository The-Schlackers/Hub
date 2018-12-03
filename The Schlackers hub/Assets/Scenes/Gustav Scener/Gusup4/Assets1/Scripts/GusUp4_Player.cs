using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GusUp4_Player : MonoBehaviour
{
    private float verticalaxis;
    private float horizontalaxis;
    [SerializeField]
    private float speed;



    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

        }

        //
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verticalaxis = Input.GetAxis("Vertical");
        if (verticalaxis != 0)
        {
            transform.position = transform.position + new Vector3(0, verticalaxis * speed);
        }

        horizontalaxis = Input.GetAxis("Horizontal");
        if (horizontalaxis != 0)
        {
            transform.position = transform.position + new Vector3(horizontalaxis * speed, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "End")
        {
            GusUp4_DataStorage.ActionIndex++;

            if(GusUp4_DataStorage.ActionIndex == 3)
            {
                if (SceneManager.GetActiveScene().name != "GusUp4_Scene_2")
                {
                    GusUp4_DataStorage.ActionIndex = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else SceneManager.LoadScene(4);
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}
