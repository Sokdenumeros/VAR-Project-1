using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public FadeScreen fadeScreen;

    void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        SceneManager.LoadScene(1);
        CameraPointerManager.paused = false;
    }
}
