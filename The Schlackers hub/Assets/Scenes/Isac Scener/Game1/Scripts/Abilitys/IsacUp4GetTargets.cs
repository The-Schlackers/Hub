using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsacUp4GetTargets : MonoBehaviour
{

    List<Collider2D> TriggerList = new List<Collider2D>();

   public List<Collider2D> Getlist
    {
        get
        {
            return TriggerList;
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        //if the object is not already in the list
        if (!TriggerList.Contains(other))
        {
            //add the object to the list
            TriggerList.Add(other);
        }
    }

    private void Start()
    {

    }

    
}
