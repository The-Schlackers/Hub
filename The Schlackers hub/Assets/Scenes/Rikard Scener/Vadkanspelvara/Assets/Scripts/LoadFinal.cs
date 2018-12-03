using System.Collections;
using UnityEngine;
using TMPro;
using myTools;

public class LoadFinal : MonoBehaviour
{
    public float fadeTime;
    private void Start()
    {
        StartCoroutine(WaitTimu(2, 3));
        StartCoroutine(WaitTimu(10, 1));
        StartCoroutine(WaitTimu(15, 2));
        StartCoroutine(WaitTimu(18, 3));
        StartCoroutine(WaitTimu(26, 1));
        StartCoroutine(WaitTimu(27, 4));
    }
    private IEnumerator WaitTimu(float wait, int nr)
    {
        yield return new WaitForSeconds(wait);
        {
            if (nr == 1)
            {
                StartCoroutine(FadeOutRoutine(GetComponent<CanvasGroup>(), GetComponent<CanvasGroup>().alpha, 0));
            }
            else if (nr == 2)
            {
                GetComponent<TextMeshProUGUI>().text = "Maybe game can be!";
            }
            else if (nr == 3)
            {
                StartCoroutine(FadeOutRoutine(GetComponent<CanvasGroup>(), GetComponent<CanvasGroup>().alpha, 1));
            }
            else if (nr == 4)
            {
                GameUtils.FadeScene(7, 3, 1, Color.white);
            }
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
