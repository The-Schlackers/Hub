using System.Collections;
using UnityEngine;
using myTools;

public class Timer : MonoBehaviour
{
    public int timeLeft = 60; //Seconds Overall
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }
    void Update()
    {
        //Showing the Score on the Canvas
        if (timeLeft == 1)
        {
            StartCoroutine(FadeOutRoutine(GetComponent<CanvasGroup>(), GetComponent<CanvasGroup>().alpha, 1));
            if (Input.anyKey)
            {
                GameUtils.FadeScene(5, 5, 3, Color.white);
            }         
        }
    }
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    private IEnumerator FadeOutRoutine(CanvasGroup cg, float start, float end, float lerpTime = 5f)
    {
        if (cg != null)
        {
            float timeStartedLerping = Time.time;
            float timeSinceStarted = Time.time - timeStartedLerping;
            float procentComplete = timeSinceStarted / lerpTime;
            bool loop = true;
            while (loop)
            {
                timeSinceStarted = Time.time - timeStartedLerping;
                procentComplete = timeSinceStarted / lerpTime;
                float maths = Mathf.Lerp(start, end, procentComplete);
                cg.alpha = maths;
                if (procentComplete >= 1)
                    loop = false;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}