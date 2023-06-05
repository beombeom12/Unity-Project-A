using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuData : MonoBehaviour
{
    public static MenuData Instance
    {
        get
        {
            if (MenuInstance == null)
            {
                MenuInstance = FindObjectOfType<MenuData>();
                if (MenuInstance == null)
                {
                    var InstanceContainer = new GameObject("MenuData");
                    MenuInstance = InstanceContainer.AddComponent<MenuData>();
                }
            }
            return MenuInstance;
        }
    }

    private static MenuData MenuInstance;


    //MenuLevel
    public int iMenuPlayerLevel = 1;
    public float fMPlayerCurrentExp = 30f;
    public float fMPlayerLvUpExp = 100f;


    //MenuEnery
    public float fMax = 100f;
    public float fCurrentEnergy = 50f;


    public float fShooterMax = 100000f;
    public float fCurrentShooter ;


    public float fDiamonMax = 200000f;
    public float fDiamonCurrent = 2000f;



    public float fShooterUI;
    public AudioClip clip;

    public void Awake()
    {
        if (MenuInstance == null)
        {
            MenuInstance = this;
            DontDestroyOnLoad(MenuInstance);
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(gameObject);

        }
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }


    public void Start()
    {
     
    }


    public void MenuPlayerExp(float fExp)
    {
        fMPlayerCurrentExp += fExp;
        if (fMPlayerCurrentExp >= fMPlayerLvUpExp)
        {
            iMenuPlayerLevel++;
            fMPlayerCurrentExp -= fMPlayerLvUpExp;
            fMPlayerLvUpExp *= 1.3f;

        }



    }

    public void MenuMinusEnergyBar(float fEnergy)
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        fCurrentEnergy -= fEnergy;

        if (fCurrentShooter <= 0f)
        {
            fCurrentShooter = 0f;

        }
    }

    
    public void MenuPlusEnergyBar(float fEnergy)
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        fCurrentEnergy += fEnergy;
        if(fCurrentEnergy >= fMax)
        {
            fCurrentEnergy = fMax;
        }
    }


    public void MenuMinusShooterBar(float fShooter)
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        fCurrentShooter -= fShooter;
        if(fCurrentShooter <= 0)
        {
            fCurrentShooter = 0;
        }
    }

    public void MenuPlusShooterBar(float fShooter)
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
        fCurrentShooter += fShooter;
        if(fCurrentShooter >= fShooterMax)
        {
            fCurrentShooter = fShooterMax;
        }
    }


    public void MenuPlusDiamondButton(float fDiamond)
    {
        SoundManager.Instance.ButtonSound("ButtonClick", clip);

        fDiamonCurrent += fDiamond;
        if (fDiamonCurrent >= fDiamonMax)
        {
            fDiamonCurrent = fDiamonMax;
        }

    }


    public void PlayerGetShooter(float fShooter)
    {
        fShooterUI += fShooter;
    }
}
