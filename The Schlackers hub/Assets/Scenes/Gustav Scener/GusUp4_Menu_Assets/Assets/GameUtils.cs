using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyTools
{
    public class GameUtils
    {
        public static void LoadScene(int index)
        {

            PlayerPrefs.SetInt("sceneToLoad", 1);

            // load loadingscene
            SceneManager.LoadScene("LoadingScene");

        }

    }
}
