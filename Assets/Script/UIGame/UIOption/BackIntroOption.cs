using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackIntroOption : MonoBehaviour
{
    public AudioClip clip;

    public GameObject OptionCanvas;

    public static BackIntroOption Instance
    {
        get
        {
            if (BackIntroInstance == null)
            {
                BackIntroInstance = FindObjectOfType<BackIntroOption>();
                if (BackIntroInstance == null)
                {
                    var InstanceContainer = new GameObject("BackIntroOption");
                    BackIntroInstance = InstanceContainer.AddComponent<BackIntroOption>();


                }
            }
            return BackIntroInstance;
        }
    }

    private static BackIntroOption BackIntroInstance;

    public GameObject QuestionPanel;


    public void GoBackForOption()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", SoundManager.Instance.bgList[23]);
        SceneManager.LoadScene("IntroMenu");
        IntroScript.Instance.MenuCanvas.SetActive(true);
     
    }

    public void BackToOptionMenu()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", SoundManager.Instance.bgList[23]);
        SceneManager.LoadScene("OptionScene");
    }
    public void GoFeedBack()
    {

     
        SoundManager.Instance.ButtonSound("ButtonClick", SoundManager.Instance.bgList[23]);
        IntroScript.Instance.PlayerInforMation();
        SceneManager.LoadScene("FeedBack");
    }


    public void GoDevel()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", SoundManager.Instance.bgList[23]);
        SceneManager.LoadScene("Developer");

    }


    public void SoundOn()
    {
        SoundManager.Instance.BgSound.volume = 1f;
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {

            SoundManager.Instance.BackGroundSoundLooping(SoundManager.Instance.bgList[0]);
            SoundManager.Instance.RePlaySound();

        }
    }



    public void MuteAll()
    {
        SoundManager.Instance.BgSound.volume = 0f;
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {

            SoundManager.Instance.BackGroundSoundStop(SoundManager.Instance.bgList[0]);
            SoundManager.Instance.MuteAll();
       
        }
    }

    public void QuestionPanelOn()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", SoundManager.Instance.bgList[23]);
        QuestionPanel.SetActive(true);
    }


    public void QuestionPanelOff()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", SoundManager.Instance.bgList[23]);
        QuestionPanel.SetActive(false);
    }
}
