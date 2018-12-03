using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsacUp4MenuPlayerScript : MonoBehaviour
{
    Vector3 TargetDirection;
    // Use this for initialization
    void Start ()
    {
        transform.Rotate(transform.forward, Random.Range(-30, 30));
        int rand = Random.Range(0, 5);

        SpriteRenderer color = GetComponent<SpriteRenderer>();

        switch(rand)
        {
            case 0:
                color.color = new Color(1, 0.5518868f, 0.5518868f, 1);
                break;

            case 1:
                color.color = new Color(0.5529412f, 0.7239975f, 1, 1);

                break;

            case 2:
                color.color = new Color(0.6159692f, 1, 0.5529412f, 1);

                break;

            case 3:
                color.color = new Color(1, 0.9804102f, 0.5529412f, 1);

                break;

            case 4:

                break;
        }
        Destroy(gameObject, 20f);
    }

   

    // Update is called once per frame
    void  FixedUpdate ()
    {
        

        GetComponent<Rigidbody2D>().AddForce(transform.up * 3000 * Time.deltaTime);
    }
}
