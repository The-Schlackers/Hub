using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GusUp4_MenuScripter : MonoBehaviour {

    public void Game1()
    {
        SceneManager.LoadScene("GusUp4_Scene_0");
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
