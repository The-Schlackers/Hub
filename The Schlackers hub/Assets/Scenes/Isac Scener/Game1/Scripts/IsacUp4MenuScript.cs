using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
//using Assets.Scripts.Utils; //GameUtils.LoadScene(x);

public class IsacUp4MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;

    [SerializeField]
    private GameObject Settings;

    [SerializeField]
    private GameObject PlayerSelect;

    [SerializeField]
    private GameObject GameStart;
    [SerializeField]
    private TextMeshProUGUI GameStartText;
    [SerializeField]
    private TextMeshProUGUI GameStartText2;
    [SerializeField]
    private TextMeshProUGUI GameStartText3;

    [SerializeField]
    private TextMeshProUGUI Currentplayer;

    private int CurrentPlayerSelect = 1;

    private int Skip = 1;
    

    void Start()
    {

        StartCoroutine(Gameentry());
        

        
    }

    


    
    private void Update()
    {
        
            
        


        if (CurrentPlayerSelect > 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //GameUtils.LoadScene(x);
        }
        if (Input.anyKeyDown)
        {
            Skip = 0;
        }
    }

    public void StartGame()
    {

        Menu.SetActive(false);
        PlayerSelect.SetActive(true);

        CurrentPlayerSelect = 1;

       
            Currentplayer.text = "Chose class, Player " + CurrentPlayerSelect;
        
    }

    public void RedSelect()
    {
        PlayerPrefs.SetInt("Player"+ CurrentPlayerSelect, 1);
        CurrentPlayerSelect++;

        Currentplayer.text = "Chose class, Player " + CurrentPlayerSelect;
    }
    public void BlueSelect()
    {
        PlayerPrefs.SetInt("Player" + CurrentPlayerSelect, 2);
        CurrentPlayerSelect++;

        Currentplayer.text = "Chose class, Player " + CurrentPlayerSelect;
    }
    public void GreenSelect()
    {
        PlayerPrefs.SetInt("Player" + CurrentPlayerSelect, 3);
        CurrentPlayerSelect++;

        Currentplayer.text = "Chose class, Player " + CurrentPlayerSelect;
    }
    public void YellowSelect()
    {
        PlayerPrefs.SetInt("Player" + CurrentPlayerSelect, 4);
        CurrentPlayerSelect++;

        Currentplayer.text = "Chose class, Player " + CurrentPlayerSelect;
    }



    public void SettingsMenu()
    {
        Menu.SetActive(false);
        Settings.SetActive(true);
    }
    public void SettingsMenuBack()
    {
        Menu.SetActive(true);
        Settings.SetActive(false);
        PlayerSelect.SetActive(false);
    }
    [SerializeField]
    private GameObject Music;
    public void QuitGame()
    {
        Destroy(Music, 2);
        SceneManager.LoadScene(2);
    }

    private IEnumerator Gameentry()
    {

        Menu.SetActive(false);
        Settings.SetActive(false);
        PlayerSelect.SetActive(false);
        GameStart.SetActive(true);

        GameStartText.text = "";
        GameStartText2.text = "";
        GameStartText3.text = "";
        yield return new WaitForSeconds(0.85f * Skip);

        GameStartText.text = "Some";

        yield return new WaitForSeconds(0.64f * Skip);

        GameStartText.text = "Some sort";

        yield return new WaitForSeconds(0.64f * Skip);

        GameStartText.text = "Some sort of";

        yield return new WaitForSeconds(0.64f * Skip);

        GameStartText2.text = "Energy";

        yield return new WaitForSeconds(0.64f * Skip);

        GameStartText2.text = "Energy Friendship";

        yield return new WaitForSeconds(0.64f * Skip);

        GameStartText3.text = "Game";

        yield return new WaitForSeconds(2.4f * Skip);

        Menu.SetActive(true);
        StartCoroutine(FadeCanvasGroup(StartgameCanvasgroup, StartgameCanvasgroup.alpha, 0, 4));

    }
    [SerializeField]
    private CanvasGroup StartgameCanvasgroup;

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


                if (procentComplete >= 1)
                {
                    GameStart.SetActive(false);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }


    }
}
