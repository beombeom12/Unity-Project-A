using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{





    public GameObject PausePanel;
    public GameObject JoystickPanel;
    public GameObject JoystickBg;
    //GamePauseSoundOn / off
    public GameObject SoundMuteButton;
    public GameObject SoundOnButton;


    public Text GetShooterText;

    //IsRightPanel?
    public GameObject PasueRightPanel;







    public AudioClip clip;




    public bool bClickDown;
    public bool bIsPause = false;


    public static GameUI Instance
    {
        get
        {
            if (GameUIInstance == null)
            {
                GameUIInstance = FindObjectOfType<GameUI>();
                if (GameUIInstance == null)
                {
                    var InstanceContainer = new GameObject("GameUI");
                    GameUIInstance = InstanceContainer.AddComponent<GameUI>();
                }
            }
            return GameUIInstance;
        }
    }

    private static GameUI GameUIInstance;


    public void Update()
    {


        RealShooter();
    }




    public void IsPause()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);

        bIsPause = !bIsPause;
        if (bIsPause)
        {
            Time.timeScale = 0f;
        }

        var PlayerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = PlayerObj.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = !bIsPause;
        }

        bClickDown = true;
        PausePanel.SetActive(true);
        JoystickPanel.SetActive(false);
        JoystickBg.SetActive(false);
    }

    public void Restart()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        bIsPause = !bIsPause;
        if (!bIsPause)
        {
            Time.timeScale = 1f;
        }

        var PlayerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = PlayerObj.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = !bIsPause;
        }

        bClickDown = true;
        PausePanel.SetActive(false);
        JoystickBg.SetActive(true);
        JoystickPanel.SetActive(true);

    }


    //게임도중 중지 버튼눌러서 확인까지 눌렀을떄 나오는 버튼 

    public void GoBackToIntroMenu()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);


        UIGameController.Instance.GameCanvase.SetActive(false);
        UIGameController.Instance.GameEnvir.SetActive(false);
        UIGameController.Instance.GamePlayer.SetActive(false);
        UIGameController.Instance.Pet1.SetActive(false);
        UIGameController.Instance.Pet2.SetActive(false);
        UIGameController.Instance.OpenDoor.SetActive(false);
        IntroScript.Instance.MenuCanvas.SetActive(true);
        IntroScript.Instance.IntroMenuCamera.SetActive(true);


        SceneManager.LoadScene("IntroMenu");
        Time.timeScale = 1f;


    }

    public void GoPausePenal()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        PasueRightPanel.SetActive(true);

    }
    public void RelCancelButtonPenal()
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        PasueRightPanel.SetActive(false);
    }

    public void RealShooter()
    {
        GetShooterText.text = "" + MenuData.Instance.fShooterUI;
    }



    public void MuteAll()
    {
        //SoundOnButton.SetActive(false);
        //SoundMuteButton.SetActive(true);
        SoundManager.Instance.BgSound.volume = 0f;
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {

            SoundManager.Instance.BackGroundSoundStop(SoundManager.Instance.bgList[1]);
            SoundManager.Instance.MuteAll();

        }

    }


    public void SoundOn()
    {
        //SoundMuteButton.SetActive(false);
        //SoundOnButton.SetActive(true);
        SoundManager.Instance.BgSound.volume = 1f;
        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {

            SoundManager.Instance.BackGroundSoundLooping(SoundManager.Instance.bgList[1]);
            SoundManager.Instance.RePlaySound();

        }

    }



    public void SoundButtonOn()
    {
        SoundOnButton.SetActive(true);
        SoundMuteButton.SetActive(false);
    }

    public void SoundMuteOn()
    {
        SoundMuteButton.SetActive(true);
        SoundOnButton.SetActive(false);
    }





}
