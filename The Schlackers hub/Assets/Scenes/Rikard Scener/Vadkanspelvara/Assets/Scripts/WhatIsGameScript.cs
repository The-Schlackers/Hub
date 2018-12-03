using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myTools;

public class WhatIsGameScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        yield return new WaitForSeconds(27);
        GameUtils.FadeScene(8,3,1,Color.white);
    }
}
