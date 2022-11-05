using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public GameObject player;


    public void Resume()
    {
        StartCoroutine(ResumeRoutine());
    }

    IEnumerator ResumeRoutine()
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);
        player.transform.position = CameraPointerManager.oldPosition;
        CameraPointerManager.paused = false;
        fadeScreen.FadeIn();
    }


    public void MainMenu()
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
