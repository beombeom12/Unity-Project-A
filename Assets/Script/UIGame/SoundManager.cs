using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public AudioSource BgSound;
    public AudioClip[] bgList;
    public static SoundManager SoundInstnace;
    public static SoundManager Instance
    {
        get
        {
            if (SoundInstnace == null)
            {
                SoundInstnace = FindObjectOfType<SoundManager>();
                if (SoundInstnace == null)
                {
                    var InstanceContainer = new GameObject("SoundManager");
                    SoundInstnace = InstanceContainer.AddComponent<SoundManager>();
             
         
                }
            }
            return SoundInstnace;
        }
    }


    public void Awake()
    {
        if (SoundInstnace == null)
        {
            SoundInstnace = this;
            DontDestroyOnLoad(SoundInstnace);
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(gameObject);

        }
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i =0; i < SoundManager.Instance.bgList.Length; i++)
        {
            if(arg0.name == SoundManager.Instance.bgList[i].name)
            {
                SoundManager.Instance.BackGroundSoundLooping(SoundManager.Instance.bgList[i]);
            }
        }
    }

    public void BackGroundSoundLooping(AudioClip clip)
    {
        BgSound.clip = clip;
        BgSound.loop = true;
        BgSound.volume = 0.5f;

        BgSound.Play();
    }

    public void BackGroundSoundStop(AudioClip clip)
    {
        BgSound.clip = clip;
        BgSound.loop = false;
        BgSound.volume = 0f;

        BgSound.Stop();
    }




    public void MuteAll()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public void RePlaySound()
    {
        DontDestroyOnLoad(SoundInstnace);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ButtonSound(string buttonsound, AudioClip audioclip)
    {
        GameObject ButtonGameOb = new GameObject(buttonsound + "Sound");
        AudioSource audiosource = ButtonGameOb.AddComponent<AudioSource>();

        audiosource.clip = audioclip;
        audiosource.Play();


        Destroy(ButtonGameOb, audioclip.length);
    }

    //게임 관련해서 전부쓰는것들 아래 쭉
    public void PlayerSound(string Playersound, AudioClip audioClip)
    {
        GameObject PlayerSoundOb = new GameObject(Playersound + "Sound");
        AudioSource audiosource = PlayerSoundOb.AddComponent<AudioSource>();

        audiosource.clip = audioClip;
        audiosource.Play();

        Destroy(PlayerSoundOb, audioClip.length);
     }

    public void MonsterSound(string MonsterSound, AudioClip audioClip)
    {
        GameObject MonsterSoundOb = new GameObject(MonsterSound + "Sound");
        AudioSource audiosource = MonsterSoundOb.AddComponent<AudioSource>();

        audiosource.clip = audioClip;
        audiosource.Play();

        Destroy(MonsterSoundOb, audioClip.length);
    }





    public void GameUISound(string GameUISound, AudioClip audioclip)
    {
        GameObject GameUISoundOb = new GameObject(GameUISound + "Sound");
        AudioSource audiosource = GameUISoundOb.AddComponent<AudioSource>();

        audiosource.clip = audioclip;
        audiosource.Play();

        Destroy(GameUISoundOb, audioclip.length);
    }


    public void GameUISoundStop(string GameUISound, AudioClip audioclip)
    {
        GameObject GameUISoundOb = new GameObject(GameUISound + "Sound");
        AudioSource audiosource = GameUISoundOb.AddComponent<AudioSource>();

        audiosource.clip = audioclip;
        audiosource.Stop();

        Destroy(GameUISoundOb, audioclip.length);
    }


}

