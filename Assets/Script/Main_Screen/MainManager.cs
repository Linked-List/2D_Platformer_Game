using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public PlayerSFXList SP;
    public Image Fade_Img;
    public Button StartBtn;
    public Button OptionBtn;
    public Button CreditBtn;
    public Button ExitBtn;
    public GameObject OptionMenu;
    public GameObject CreditMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBtnPress()
    {
        SP.PlayerSFX_SoundPlay(0);

        StartCoroutine(FadeInStart(2f, true));
    }

    public void OptionBtnPress()
    {
        SP.PlayerSFX_SoundPlay(0);

        OptionMenu.SetActive(true);

        StartBtn.interactable = false;
        OptionBtn.interactable = false;
        CreditBtn.interactable = false;
        ExitBtn.interactable = false;
    }

    public void CreditBtnPress()
    {
        SP.PlayerSFX_SoundPlay(0);

        CreditMenu.SetActive(true);

        StartBtn.interactable = false;
        OptionBtn.interactable = false;
        CreditBtn.interactable = false;
        ExitBtn.interactable = false;
    }

    public void ExitBtnPress()
    {
        SP.PlayerSFX_SoundPlay(0);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
        #else
        Application.Quit();
        
        #endif
    }

    public void OptionExitBtnPress()
    {
        SP.PlayerSFX_SoundPlay(1);
        OptionMenu.SetActive(false);

        StartBtn.interactable = true;
        OptionBtn.interactable = true;
        CreditBtn.interactable = true;
        ExitBtn.interactable = true;
    }

    public void CreditExitBtnPress()
    {
        SP.PlayerSFX_SoundPlay(1);

        CreditMenu.SetActive(false);

        StartBtn.interactable = true;
        OptionBtn.interactable = true;
        CreditBtn.interactable = true;
        ExitBtn.interactable = true;
    }

    IEnumerator FadeOut(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        while (t < fadeTime)
        {
            //Update t value.
            t += Time.deltaTime;

            //Calculate 진행%.
            float percent = t / fadeTime;      //Mathf.Clamp()

            //Update 투명도.
            if (isFadeEnded)
                Fade_Img.color = new Color(Fade_Img.color.r,
                                           Fade_Img.color.g,
                                           Fade_Img.color.b,
                                           Mathf.Lerp(1f, 0, percent));

            yield return null;
        }

        isFadeEnded = false;
    }

    IEnumerator FadeInStart(float fadeTime, bool isFadeEnded)
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

        isFadeEnded = false;
        SceneManager.LoadScene("Stage_0");
    }
}
