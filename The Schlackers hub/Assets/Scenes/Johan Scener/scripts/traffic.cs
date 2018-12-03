
using UnityEngine;
using System.Collections;

public class traffic : MonoBehaviour
{
    public Transform[] spawnLocation;
    public GameObject[] whattospawn;
    public GameObject[] whatsospawnagain;
    public Rigidbody test;
    public float MoveSpeed = 20f;
    public bool alive = true;
    

    private void Start()
    {
        whatsospawnagain = new GameObject[4];
        Spawn();
        

    }

    void Update()
    {
        StartCoroutine(Moveing());
    }
    void Spawn()
    {
        whatsospawnagain[0] = Instantiate(whattospawn[0], spawnLocation[0].transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        whatsospawnagain[1] = Instantiate(whattospawn[0], spawnLocation[1].transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
        whatsospawnagain[2] = Instantiate(whattospawn[0], spawnLocation[2].transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;
        whatsospawnagain[3] = Instantiate(whattospawn[0], spawnLocation[3].transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;
    }
    

     IEnumerator Moveing()
     {
        MoveSpeed = 20f;
        for (int i = 0; i < whatsospawnagain.Length; i++)
        {
            test = whatsospawnagain[i].GetComponent<Rigidbody>();
            //test.AddForce(test.transform.forward * 11f);
            MoveSpeed = MoveSpeed + Random.Range(0.0f, 14.0f);
            test.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

        }
        yield return new WaitForSeconds(8);
    }

    public void GoBack()
    {
       


    }

    //void OnCollisionEnter(Collision col)
    //{

    //    if (col.gameObject.tag == "Respawn")
    //    {
    //        test = whatsospawnagain[1].GetComponent<Rigidbody>();
    //        if (gameObject == test)
    //        {
    //            test.transform.Rotate(0, 90, 0);
    //        }            

            

    //    }
    //}



}