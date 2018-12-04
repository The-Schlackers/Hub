using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsacUp4HealthbarScript : MonoBehaviour
{

    private GameObject FollowPlayer;
    public GameObject SetFollowPlayer
    {
        set
        {
            FollowPlayer = value;
        }
    }

    [SerializeField]
    private float FollowForce;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (tag != "Shield")
        {
            if (FollowPlayer != null)
                transform.position = Vector3.Lerp(transform.position, FollowPlayer.transform.position, FollowForce);
            else
                Destroy(gameObject);
        }
        else
        {
            if (FollowPlayer != null)
            {
                transform.position = Vector3.Lerp(transform.position, FollowPlayer.transform.position, FollowForce);
                transform.rotation = Quaternion.Lerp(transform.rotation, FollowPlayer.transform.rotation, FollowForce);
            }
                
            else
                Destroy(gameObject);
        }
       
        
        
	}
}
