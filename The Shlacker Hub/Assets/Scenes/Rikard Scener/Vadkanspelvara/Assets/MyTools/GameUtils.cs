using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace myTools
{
    public class GameUtils :MonoBehaviour
    {
        public static void LoadScene(int index)
        {
            Debug.Log("Starting game");

            PlayerPrefs.SetInt("sceneToLoad", index);
            PlayerPrefs.Save(); 

            SceneManager.LoadScene(15);
        }
        //Create Instance
        private static GameUtils Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (new GameObject("GameUtils")).AddComponent<GameUtils>();
                }
                return m_Instance;
            }
        }
        //Loading Fade/Screen Variable Initialization
        private static GameUtils m_Instance = null;
        private int m_LevelIndex = 0;
        //Loading Fade Variable Initialization
        private Material m_Material = null;
        private Material m_LoadMat = null;
        private bool m_Fading = false;
        //Create Materials
        private void Awake()
        {
            DontDestroyOnLoad(this);
            m_Instance = this;
            m_Material = Resources.Load<Material>("Plane_No_zTest"); //Black Screen
#if UNITY_EDITOR
            if (m_Material == null)
            {
                var resDir = new System.IO.DirectoryInfo(System.IO.Path.Combine(Application.dataPath, "Resources"));
                if (!resDir.Exists)
                    resDir.Create();
                Shader s = Shader.Find("Plane/No zTest");
                if (s == null)
                {
                    string shaderText = "Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }";
                    string path = System.IO.Path.Combine(resDir.FullName, "Plane_No_zTest.shader");
                    Debug.Log("Shader missing, create asset: " + path);
                    System.IO.File.WriteAllText(path, shaderText);
                    UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceSynchronousImport);
                    UnityEditor.AssetDatabase.LoadAssetAtPath<Shader>("Resources/Plane_No_zTest.shader");
                    s = Shader.Find("Plane/No zTest");
                }
                var mat = new Material(s);
                mat.name = "Plane_No_zTest";
                UnityEditor.AssetDatabase.CreateAsset(mat, "Assets/Resources/Plane_No_zTest.mat");
                m_Material = mat;

            }
#endif
        }

        //Load With Fade
        public static bool Fading
        {
            get { return Instance.m_Fading; }
        }
        private void DrawQuad(Color aColor, float aAlpha)
        {
            aColor.a = aAlpha;
            m_Material.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Begin(GL.QUADS);
            GL.Color(aColor);
            GL.Vertex3(0, 0, -1);
            GL.Vertex3(0, 1, -1);
            GL.Vertex3(1, 1, -1);
            GL.Vertex3(1, 0, -1);
            GL.End();
            GL.PopMatrix();
        }
        private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
        {
            float t = 0.0f;
            while (t < 1.0f)
            {
                yield return new WaitForEndOfFrame();
                t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
                DrawQuad(aColor, t);
            }
            SceneManager.LoadScene(m_LevelIndex);
            while (t > 0.0f)
            {
                yield return new WaitForEndOfFrame();
                t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
                DrawQuad(aColor, t);
            }
            m_Fading = false;
        }
        private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
        {
            m_Fading = true;
            StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
        }
        public static void FadeScene(int aLevelIndex, float aFadeOutTime, float aFadeInTime, Color aColor)
        {
            if (Fading) return;
            Instance.m_LevelIndex = aLevelIndex;
            Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
        }
    }
}
