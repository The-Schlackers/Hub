using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ReturnHome);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReturnHome()
    {
        GameUtils.LoadScene(0);
    }
}
