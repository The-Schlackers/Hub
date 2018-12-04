using System.Collections;
using UnityEngine;
using myTools;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(WaitTimu(2, 1));
    }
    private IEnumerator WaitTimu(float wait, int nr)
    {
        yield return new WaitForSeconds(wait);
        {
            if (nr == 1)
            GameUtils.FadeScene(5, 3, 1, Color.white);
        }
    }
}

