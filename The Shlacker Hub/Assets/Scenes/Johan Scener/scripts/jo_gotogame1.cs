using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jo_gotogame1 : MonoBehaviour {

    public void Start()
    {

    }

    public void Update()
    {

    }

    public void NextScene(string scene)
    {
        SceneManager.LoadScene("Main_Scene");
    }
}
