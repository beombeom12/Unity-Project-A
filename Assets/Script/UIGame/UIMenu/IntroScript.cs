using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{
    public static IntroScript Instance
    {
        get
        {
            if (IntroScripInstance == null)
            {
                IntroScripInstance = FindObjectOfType<IntroScript>();
                if (IntroScripInstance == null)
                {
                    var InstanceContainer = new GameObject("IntroScript");
                    IntroScripInstance = InstanceContainer.AddComponent<IntroScript>();


                }
            }
            return IntroScripInstance;
        }
    }


    private static IntroScript IntroScripInstance;
   // public GameObject LogoPenal;
    public GameObject MenuPenal;
    public GameObject WorldEnergyAnim;
    public GameObject MenuCanvas;
    public GameObject IntroMenuCamera;



    public AudioClip GameClip;
    public AudioClip clip;

    //무기 각종 관련 데이터 전달
    public float fWeaponPlusDamage;
    //갑옷
    public float fArmorPlusHp;
    public float fArmorPlusMaxHp;
    //반지 첫번째
    public float fRightRingDamage;
    public float fRightRingHp;
    public float fRightRingMaxHp;

    //반지 두뻔째
    public float fLeftRingDamage;
    public float fLeftRingHp;
    public float fLeftRingMaxHp;

    //첫번째 펫

    public float fFirstPetDamage;
    public float fFirstPetHp;
    public float fFirstPetMaxHp;

    //두번째 펫
    public float fSecondPetDamage;
    public float fSecondPetHp;
    public float fSecondPetMaxHp;


    public void Awake()
    {
        if(IntroScripInstance == null)
        {
            IntroScripInstance = this;
            DontDestroyOnLoad(IntroScripInstance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
              StartCoroutine(DelayTime(3));
    }


    // Start is called before the first frame update
    void Start()
    {
  
    }


    public void Update()
    {
        PlayerInforMation();
    }


    IEnumerator DelayTime(float fTime)
    {
        yield return new WaitForSeconds(fTime);
     //   LogoPenal.SetActive(false);
        //MenuPenal.SetActive(true);
    }
    public void GoGameScene()
    {
        SoundManager.Instance.GameUISound("Click", SoundManager.Instance.bgList[23]);
        PlayerInforMation();

        Instantiate(WorldEnergyAnim, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        

        StartCoroutine(GoWaitforGameScene());

    }

    IEnumerator GoWaitforGameScene()
    {
        yield return null;

        yield return new WaitForSeconds(0.83f);
        MenuData.Instance.MenuMinusEnergyBar(5f);
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Archero");
        IntroMenuCamera.SetActive(false);
        MenuCanvas.SetActive(false);
    }

    public void GoLandScene()
    {
        PlayerInforMation();
        SceneManager.LoadScene("MultiLanding");
        SoundManager.Instance.ButtonSound("ButtonClick", clip);
    }

    public void GoOptionScene()
    {
        MenuCanvas.SetActive(false);
        SceneManager.LoadScene("OptionScene");
    }





    public static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }




    public void PlayerInforMation()
    {

        //무기
        EquipmentSlot slot = EquipMent.Instance.EquipmentSlots[0].GetComponent<EquipmentSlot>();
        fWeaponPlusDamage = slot.item.fDamage + slot.item.fLevelUpPlusDamage;

        //갑옷

        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[1].GetComponent<EquipmentSlot>();
        fArmorPlusHp = slots.item.fHp + slots.item.fLevelUpPlusHp;
        fArmorPlusMaxHp = slots.item.fMaxHp + slots.item.fLevelUpPlusMaxHp;


        //반지 첫번째

        EquipmentSlot Rightring = EquipMent.Instance.EquipmentSlots[2].GetComponent<EquipmentSlot>();
        fRightRingDamage = Rightring.item.fDamage + Rightring.item.fLevelUpPlusDamage;
        fRightRingHp = Rightring.item.fHp + Rightring.item.fLevelUpPlusHp;
        fRightRingMaxHp = Rightring.item.fMaxHp + Rightring.item.fLevelUpPlusMaxHp;



        //반지 두번째

        EquipmentSlot LeftRing = EquipMent.Instance.EquipmentSlots[3].GetComponent<EquipmentSlot>();
        fLeftRingDamage = LeftRing.item.fDamage + LeftRing.item.fLevelUpPlusDamage;
        fLeftRingHp = LeftRing.item.fHp + LeftRing.item.fLevelUpPlusHp;
        fLeftRingMaxHp = LeftRing.item.fMaxHp + LeftRing.item.fLevelUpPlusMaxHp;


        //첫번째 펫

        EquipmentSlot FirstPets = EquipMent.Instance.EquipmentSlots[4].GetComponent<EquipmentSlot>();

        fFirstPetDamage = FirstPets.item.fDamage + FirstPets.item.fLevelUpPlusDamage;
        fFirstPetHp = FirstPets.item.fHp + FirstPets.item.fLevelUpPlusHp;
        fFirstPetMaxHp = FirstPets.item.fMaxHp + FirstPets.item.fLevelUpPlusMaxHp;

        EquipmentSlot SecondPets = EquipMent.Instance.EquipmentSlots[5].GetComponent<EquipmentSlot>();

        fSecondPetDamage = SecondPets.item.fDamage + SecondPets.item.fLevelUpPlusDamage;
        fSecondPetHp = SecondPets.item.fHp + SecondPets.item.fLevelUpPlusHp;
        fSecondPetMaxHp = SecondPets.item.fMaxHp + SecondPets.item.fLevelUpPlusMaxHp;

    }
}


