using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IsacUp4GameScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI WaveCounter;
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject HealthbarPrefab;
    [SerializeField]
    private GameObject EnemyPrefab;

    private GameObject[] Player = new GameObject[3];
    private GameObject[] Hb = new GameObject[3];


    [SerializeField]
    private float SpawnRate;
    private float SpawnTimer;

    [SerializeField]
    private float MinSpawnDistance;
    [SerializeField]
    private float MaxSpawnDistance;

    [SerializeField]
    private CanvasGroup GameoverCG;

    private bool GameisOver;

    private bool GmEndless = false;
    private bool GmWaves = true;

    private bool WaveComplete = true;
    private int CurrentWave;

    private int EnemysLeft = 0;


    // Use this for initialization
    void Start ()
    {
        CurrentWave = 1;
        GameisOver = false;

        for (int i = 0; i < 3; i++)
        {
            Player[i] = Instantiate<GameObject>(PlayerPrefab, Vector3.right * (-2 + 2 * i), Quaternion.identity);
            Hb[i] = Instantiate<GameObject>(HealthbarPrefab, Vector3.right * (-2 + 2 * i), Quaternion.identity);

            if (PlayerPrefs.GetInt("Player" + (i + 1)) == 1)
            {
                Color red = new Color(1, 0.5518868f, 0.5518868f, 1);

                if (Player[i].GetComponent<SpriteRenderer>() != null)
                Player[i].GetComponent<SpriteRenderer>().color = red;

                Hb[i].GetComponentsInChildren<Image>()[1].color = red;
                Hb[i].GetComponentsInChildren<Image>()[0].color = red - new Color(0,0,0,0.7f);




            }

            else if (PlayerPrefs.GetInt("Player" + (i + 1)) == 2)
            {
                Color blue = new Color(0.5529412f, 0.7239975f, 1, 1);

                if (Player[i].GetComponent<SpriteRenderer>() != null)
                    Player[i].GetComponent<SpriteRenderer>().color = blue;

                Hb[i].GetComponentsInChildren<Image>()[1].color = blue;
                Hb[i].GetComponentsInChildren<Image>()[0].color = blue - new Color(0, 0, 0, 0.7f);

            }

            else if (PlayerPrefs.GetInt("Player" + (i + 1)) == 3)
            {
                Color green = new Color(0.6159692f, 1, 0.5529412f, 1);

                if (Player[i].GetComponent<SpriteRenderer>() != null)
                    Player[i].GetComponent<SpriteRenderer>().color = green;

                Hb[i].GetComponentsInChildren<Image>()[1].color = green;
                Hb[i].GetComponentsInChildren<Image>()[0].color = green - new Color(0, 0, 0, 0.7f);


            }

            else if (PlayerPrefs.GetInt("Player" + (i + 1)) == 4)
            {
                Color yellow = new Color(1, 0.9804102f, 0.5529412f, 1);

                    if (Player[i].GetComponent<SpriteRenderer>() != null)
                    Player[i].GetComponent<SpriteRenderer>().color = yellow;

                Hb[i].GetComponentsInChildren<Image>()[1].color = yellow;
                Hb[i].GetComponentsInChildren<Image>()[0].color = yellow - new Color(0, 0, 0, 0.7f);

            }

            Player[i].GetComponent<IsacUp4PlayerScript>().Setplayer = i + 1;
            Hb[i].GetComponent<IsacUp4HealthbarScript>().SetFollowPlayer = Player[i];

            Player[i].GetComponent<IsacUp4PlayerScript>().SetHealthBar = Hb[i].GetComponentInChildren<Slider>();

            Player[i].GetComponent<IsacUp4PlayerScript>().SetHp();
            Player[i].GetComponent<IsacUp4PlayerScript>().SetGm = gameObject;

            
        }


        
    }

    List<GameObject> Enemys = new List<GameObject>();

    private void Update()
    {
        SpawnTimer += Time.deltaTime;




        if (GmEndless)
        {


            if (SpawnTimer >= SpawnRate)
            {
                SpawnTimer = 0;


                Vector3 pos = Vector3.zero;
                pos.x = (Random.Range(0, 2) * 2 - 1) * Random.Range(MinSpawnDistance, MaxSpawnDistance);
                pos.y = (Random.Range(0, 2) * 2 - 1) * Random.Range(MinSpawnDistance, MaxSpawnDistance);

                GameObject Enemy = Instantiate<GameObject>(EnemyPrefab, pos, Quaternion.identity);
                GameObject EnemyHb = Instantiate<GameObject>(HealthbarPrefab, pos, Quaternion.identity);

                EnemyHb.GetComponent<IsacUp4HealthbarScript>().SetFollowPlayer = Enemy;

                Enemy.GetComponent<IsacUp4EnemyScript>().SetHealthBar = EnemyHb.GetComponentInChildren<Slider>();
            }
        }
       
        if (GmWaves)
        {

            if (SpawnTimer > 10)
            {
                EnemysLeft = 0;
                for ( int i = 0; i < Enemys.Count; i++)
                {
                    if (Enemys[i] != null)
                        EnemysLeft += 1;
                }
                if (EnemysLeft <= 0)
                    WaveComplete = true;
            }

            if (WaveComplete)
            {
                
                WaveCounter.text = "Wave: " + CurrentWave.ToString();
                
                for (int i = 0; i < CurrentWave + 1f; i++)
                {
                    int Enemytype = 0;

                    if (CurrentWave > 7)
                    {
                        Enemytype = Random.Range(0, 3);
                    }
                    else if (CurrentWave > 3)
                    {
                        Enemytype = Random.Range(0, 2);
                    }


                    #region Pos

                    Vector3 pos = Vector3.zero;
                    pos.x = (Random.Range(0, 2) * 2 - 1)* Random.Range(MinSpawnDistance, MaxSpawnDistance);
                    pos.y = (Random.Range(0, 2) * 2 - 1) * Random.Range(MinSpawnDistance, MaxSpawnDistance);
                    
                    #endregion

                    GameObject Enemy = Instantiate<GameObject>(EnemyPrefab, pos, Quaternion.identity);
                    GameObject EnemyHb = Instantiate<GameObject>(HealthbarPrefab, pos, Quaternion.identity);

                    if (Enemytype == 2)
                    {
                        Enemy.name = "Healer";
                    }

                    EnemyHb.GetComponent<IsacUp4HealthbarScript>().SetFollowPlayer = Enemy;

                    Enemy.GetComponent<IsacUp4EnemyScript>().SetHealthBar = EnemyHb.GetComponentInChildren<Slider>();
                    Enemy.GetComponent<IsacUp4EnemyScript>().SetHp();
                    Enemys.Add(Enemy);

                    Enemy.GetComponent<IsacUp4EnemyScript>().SetGm = gameObject;

                    #region random attributes

                    float rand = Random.Range(4.5124f - (0.3f - Mathf.Min(CurrentWave*0.5f , 1.2f)), 4.5124f + (0.3f + Mathf.Min(CurrentWave * 0.5f, 2.2f)));

                    Enemy.transform.localScale = new Vector3(rand,rand,rand);

                    Enemy.GetComponent<IsacUp4EnemyScript>().Movespeed1 += (4.5124f + (0.3f + Mathf.Min(CurrentWave * 0.5f, 2.2f)) - rand) * 700;
                    Enemy.GetComponent<IsacUp4EnemyScript>().MaxHealth1 -= (4.5124f + (0.3f + Mathf.Min(CurrentWave * 0.5f, 2.2f)) - rand) * 14;
                    Enemy.GetComponent<IsacUp4EnemyScript>().Class = Enemytype;

                    Enemy.GetComponent<IsacUp4EnemyScript>().SetHp();

                    #endregion


                }

                if (CurrentWave%5 == 0)
                {
                    #region Pos

                    Vector3 pos = Vector3.zero;
                    pos.x = (Random.Range(0, 2) * 2 - 1) * Random.Range(MinSpawnDistance, MaxSpawnDistance);
                    pos.y = (Random.Range(0, 2) * 2 - 1) * Random.Range(MinSpawnDistance, MaxSpawnDistance);

                    #endregion

                    GameObject Enemy = Instantiate<GameObject>(EnemyPrefab, pos, Quaternion.identity);
                    GameObject EnemyHb = Instantiate<GameObject>(HealthbarPrefab, pos, Quaternion.identity);

                    EnemyHb.GetComponent<IsacUp4HealthbarScript>().SetFollowPlayer = Enemy;

                    Enemy.GetComponent<IsacUp4EnemyScript>().SetHealthBar = EnemyHb.GetComponentInChildren<Slider>();
                    Enemy.GetComponent<IsacUp4EnemyScript>().SetHp();
                    Enemys.Add(Enemy);
                    Enemy.GetComponent<IsacUp4EnemyScript>().SetGm = gameObject;

                    float rand = Random.Range(20 + CurrentWave , 20 + CurrentWave *1.3f);

                    Enemy.transform.localScale = new Vector3(rand, rand, rand);

                    Enemy.GetComponent<IsacUp4EnemyScript>().Movespeed1 -= rand * 14;
                   Enemy.GetComponent<IsacUp4EnemyScript>().MaxHealth1 += 100 * (CurrentWave/1.5f);
                    Enemy.GetComponent<IsacUp4EnemyScript>().Class = 10;

                    Enemy.GetComponent<IsacUp4EnemyScript>().SetHp();
                }

                CurrentWave++;

                WaveComplete = false;
            }
        }
    }


    public void GameOver()
    {
        if(!GameisOver)
        {
            GameisOver = true;
            Time.timeScale = 0.35f;
            StartCoroutine(FadeCanvasGroup(GameoverCG, GameoverCG.alpha, 1, 1.8f));
            StartCoroutine(Exit());
        }

    }

    private IEnumerator Exit()
    {
        yield return new WaitForSecondsRealtime(5);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 10f)
    {
        if (cg != null)
        {
            float timeStartedLerping = Time.time;
            float timeSinceStarted = Time.time - timeStartedLerping;
            float procentComplete = timeSinceStarted / lerpTime;

            while (true)
            {
                timeSinceStarted = Time.time - timeStartedLerping;
                procentComplete = timeSinceStarted / lerpTime;

                float currentValue = Mathf.Lerp(start, end, procentComplete);
                cg.alpha = currentValue;


                if (procentComplete >= 1) break;

                yield return new WaitForEndOfFrame();
            }
        }


    }

    
}
