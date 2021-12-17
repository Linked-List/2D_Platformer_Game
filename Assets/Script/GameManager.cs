using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public BGMList BP;

    public Image Fade_Img;
    public GameObject GameClearUI;
    public GameObject GameOverUI;
    public GameObject GameUI;

    public int stageLevel;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showStageClear()
    {
        BP.BGM_SoundPlay(0);

        StartCoroutine(GameFadeIn(2f, true));

        GameClearUI.SetActive(true);

        StartCoroutine(Nextstage());

    }

    public void showGameOver()
    {
        BP.BGM_SoundPlay(1);

        StartCoroutine(GameFadeIn(2f, true));

        GameOverUI.SetActive(true);
    }

    public void PressYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PressNo()
    {
        SceneManager.LoadScene("Main");
    }

    IEnumerator GameFadeIn(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float percent = t / fadeTime;

            if (isFadeEnded)
                Fade_Img.color = new Color(Fade_Img.color.r,
                                           Fade_Img.color.g,
                                           Fade_Img.color.b,
                                           Mathf.Lerp(0, 1f, percent));
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        isFadeEnded = false;
    }

    IEnumerator Nextstage()
    {
        stageLevel ++;

        yield return new WaitForSeconds(4.5f);

        if (stageLevel == 6)
        {
            SceneManager.LoadScene("Main");
        }

        else
        {
            SceneManager.LoadScene(stageLevel, LoadSceneMode.Single);
        }
    }
}
