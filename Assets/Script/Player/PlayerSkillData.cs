using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerSkillData : MonoBehaviour
{
    public static PlayerSkillData Instance
    {
        get
        {
            if (PlayerSkillInstance == null)
            {
                PlayerSkillInstance = FindObjectOfType<PlayerSkillData>();
                if (PlayerSkillInstance == null)
                {
                    var InstanceContainer = new GameObject("PlayerSkillData");
                    PlayerSkillInstance = InstanceContainer.AddComponent<PlayerSkillData>();
                }
            }
            return PlayerSkillInstance;
        }
    }
    private static PlayerSkillData PlayerSkillInstance;


    //Player
    public float fDamage = 150f;
    public float fSphereDamage = 50f;
    public float fPlayerMaxHp = 1000f;
    public float fPlayerCurrentHp = 1000f;
    public GameObject Player;


    // public GameObject PlayerWeapon;
  
    public GameObject[] PlayerWeapon;
    public GameObject[] PlayerBowWeapon;


    public GameObject SphereWeapon;
    public GameObject CoinExp;
    public GameObject PlayerHealthPack;



    public GameObject[] OutlineChangeWeapon;


    public List<int> PlayerSkill = new List<int>();


    public List<int> BowPlayerSkill = new List<int>();

    //ShooterUI

    public int PlayerLevel = 1;
    public float fShooterUI = 0f;
    public float PlayerCurrentExp = 0f;
    public float PlayerLvUpExp = 500f;

    public bool bPlayerDead = false;


    public float fHealthPack = 100f;



    //Pet
    public float fPetDamage = 100f;

    public float fBatPetWeaponDamage = 50f;
    public GameObject PetWeapon;

  

    public void Start()
    {
 
        fDamage = fDamage + IntroScript.Instance.fWeaponPlusDamage
    + IntroScript.Instance.fRightRingDamage + IntroScript.Instance.fLeftRingDamage
    + IntroScript.Instance.fFirstPetDamage + IntroScript.Instance.fSecondPetDamage
    + ButtonController.Instance.fPower + ButtonController.Instance.fAllPower;

        fPlayerCurrentHp = fPlayerCurrentHp + IntroScript.Instance.fArmorPlusHp
             + IntroScript.Instance.fRightRingHp + IntroScript.Instance.fLeftRingHp
             + IntroScript.Instance.fFirstPetHp + IntroScript.Instance.fSecondPetHp
             + ButtonController.Instance.fHp + ButtonController.Instance.fAllHp;
       

        fPlayerMaxHp = fPlayerMaxHp + IntroScript.Instance.fArmorPlusMaxHp
            + IntroScript.Instance.fRightRingMaxHp + IntroScript.Instance.fLeftRingMaxHp
            + IntroScript.Instance.fFirstPetMaxHp + IntroScript.Instance.fSecondPetMaxHp
            + ButtonController.Instance.fMaxHp + ButtonController.Instance.fAllMaxHp;


        fHealthPack = fHealthPack + ButtonController.Instance.Healthpack;

    }



    private void Update()
    {
        if (fPlayerCurrentHp <= 0f)
        {
            fPlayerCurrentHp = 0f;


            //Instantiate(EffectScript.Instance.PlayerDeadEffect, Player.transform.position, Player.transform.rotation);
        }
     
        GetChangeWeapon();

    }





    public void PlayerExp(float fExp)
    {
        PlayerCurrentExp += fExp;
        if (PlayerCurrentExp >= PlayerLvUpExp)
        {
            PlayerLevel++;
            PlayerCurrentExp -= PlayerLvUpExp;
           // PlayerLvUpExp *= 1.3f;
            StartCoroutine(PlayerLevelUp());
        }
    }


    public void ItemHealthpack()
    {
        
        fPlayerCurrentHp +=  fHealthPack;
  
        SoundManager.Instance.PlayerSound("PlayerHealthRecovery", SoundManager.Instance.bgList[13]);
        if(fPlayerCurrentHp >= fPlayerMaxHp)
        {
            fPlayerCurrentHp = fPlayerMaxHp;
        }
    }


 


    IEnumerator PlayerLevelUp()
    {
        Instantiate(EffectScript.Instance.PlayerLevelUpEffect, Player.transform.position, Player.transform.rotation);
        Instantiate(EffectScript.Instance.PlayerLevelUpTextEffect, new Vector3(Player.transform.position.x, Player.transform.position.y + 1f, Player.transform.position.z), Quaternion.Euler(90f, 0f, 0f));
        SoundManager.Instance.PlayerSound("PlayerLevelUp", SoundManager.Instance.bgList[5]);
        yield return new WaitForSeconds(2f);
        UIGameController.Instance.PlayerLevelUp(true);

    }

    public void PlayerTakeDamage(float fDamage)
    {

        fPlayerCurrentHp -= fDamage;

        SoundManager.Instance.PlayerSound("PlayerBodyHittedChange", SoundManager.Instance.bgList[14]);
        if (fPlayerCurrentHp <= 0)
        {
            UIGameController.Instance.EndGame();

      
        }

    }


    public void GetChangeWeapon()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            OutlineChangeWeapon[0].SetActive(true);
            OutlineChangeWeapon[1].SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            OutlineChangeWeapon[0].SetActive(false);
            OutlineChangeWeapon[1].SetActive(true);
        }
    }

}



