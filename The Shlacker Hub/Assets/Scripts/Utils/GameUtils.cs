using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utils
{
    class GameUtils
    {
        public static void LoadScene(int index)
        {
            PlayerPrefs.SetInt("SceneToLoad", index);
            SceneManager.LoadScene(1);


            if (index >= 2 && index <=6)
            PlayerPrefs.SetInt("LastLoadedScene", index);
        }
    }
}
