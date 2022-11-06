using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public FadeScreen fadeScreen;

    private void interaction()
    {
        StartCoroutine(MainMenuRoutine());
    }


    IEnumerator MainMenuRoutine()
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.LoadScene(0);
    }
}
