using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance
    {
        get
        {
            if (InventroyInstance == null)
            {
                InventroyInstance = FindObjectOfType<InventoryManager>();
                if (InventroyInstance == null)
                {
                    var InstanceContainer = new GameObject("InventoryManager");
                    InventroyInstance = InstanceContainer.AddComponent<InventoryManager>();
                }
            }
            return InventroyInstance;
        }
    }

    private static InventoryManager InventroyInstance;

    public GameObject ItemInformationPenal;

    //구매못할시 나올 패널
    public GameObject[] NotEnoughtMoneyPeanl;
    //프리팹 관련 부모프리팹과 자식 프리팹
    public GameObject[] ParentPenalPC;
    public GameObject PenalPCPrefabs;

    public List<GameObject> PenalSlots = new List<GameObject>();

    
    //Text Collect

    public Text PlayerDamageText;
    public Text PlayerHpText;

    //Equipment for Penal Description;
    //이름
    public Text[] EquipNameText;
    //레벨
    public Text[] EquipLevelPenalText;
    //등급
    public Text[] EquipGradeText;
    //설명
    public Text[] EquipDescriptionText;
    //속성
    public Text[] EquipInformationText;
    //부연설명
    public Text[] EquipSubDescriptionText;
    //데미지
    public Text[] EquipDamageText;
    //레벨업시추가데미지
    public Text[] EquipLevelUpPlusDamageText;

    //아래 속성 무기스크롤쪽.
    //업그레이드 재료
    public Text[] EquipUpdageMaterialText;
    //무기스크롤.
    public Text[] EquipWeaponMaterialText;
    //패널안 레벨업 버튼 누르면 20 단위로 커지게 만들어야함.
    public Text[] EquipButtonLevelUpText;
    //HP관련된것 
    public Text[] EquipHpText;
    public Text[] EquipLevelUpHpText;


    //레벨업시 버튼 증가
    private float fLevelUpPlusing = 200f;
    
    //레벨업시 증가할 데미지들
    private float fEquipDamage = 30f;
    private float fLevelUpEuqipDamage = 3f;

    //레벨업시 증가할 HP
    private float fEquipHp = 50f;
    private float fLevelUpEquipHp = 10f;
    private float fEquipMaxHp = 50f;
    private float fLevelUpEquipMaxhp = 10f;

    public AudioClip clip;







    private const int slotCount = 1;

    private const int index = 12;


    public void Awake()
    {
        if (InventroyInstance == null)
        {
            InventroyInstance = this;
            DontDestroyOnLoad(InventroyInstance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {

    }
    private void Start()
    {



    }


    public void Update()
    {
        //장비창에서 플레이어 정보 불러오기
        UpdateUI();
        PlayerInformationInfo();
        EquipTextCollecting(Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex]);
       
    }


    //장비패널을 끄는 버튼 
    public void ItemInformationGetInfo()
    {

        Inventory.Instance.slotPenals[Inventory.Instance.selectedSlotIndex].SetActive(false);
       // Inventory.Instance.UIPlayer.SetActive(true);



        PenalSlots.Clear();

        SoundManager.Instance.ButtonSound("ButtonClick", clip);
    }

    public void GetInfoPenalSlot()
    {
        GameObject slot = Instantiate(PenalPCPrefabs, ParentPenalPC[Inventory.Instance.selectedSlotIndex].transform);
        PenalSlots.Add(slot);
        
    }

    //장비창의 플레이어 데미지 HP정보 불러오기 
    public void PlayerInformationInfo()
    {


        PlayerDamageText.text = "" + PlayerSkillData.Instance.fDamage;

        PlayerHpText.text = "" + PlayerSkillData.Instance.fPlayerMaxHp;
    }


    //레벨업시 슈터가 빠지는것을 두배씩 늘린다.
    public void LevelUpShooterMinus(Item item)
    {
        if(item.quantity < item.Maxquantity)
        {
          item.quantity += 1;
        }
        else if(item.quantity >= item.Maxquantity)
        {
            item.Maxquantity = item.quantity;
        }




        for (int i = 0; i < item.quantity; i++)
        {
            item.fLevelShootButton =  fLevelUpPlusing + item.fLevelShootButton;
       
        }


        if (item != null && item.itemType == ItemType.Weapon)
        {
            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fDamage += fEquipDamage
            + Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusDamage;

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusDamage += fLevelUpEuqipDamage;
        }
        else if(item != null && item.itemType == ItemType.Armor)
        {
            //HP
            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp += (fEquipHp +
                Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusHp);

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusHp += fLevelUpEquipHp;

            //MaxHP

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fMaxHp += fEquipMaxHp +
                Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusMaxHp;

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusMaxHp += fLevelUpEquipMaxhp;



        }
        else if(item != null && item.itemType == ItemType.RightRing || item.itemType == ItemType.LeftRing 
            || item.itemType == ItemType.RightPet || item.itemType == ItemType.LeftPet)
        {
            //damage
            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fDamage += fEquipDamage
            + Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusDamage;
            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusDamage += fLevelUpEuqipDamage;


            //HP
            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp += fEquipHp +
                Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusHp;

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusHp += fLevelUpEquipHp;

            //MaxHP

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fMaxHp += fEquipMaxHp +
                Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusMaxHp;

            Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fLevelUpPlusMaxHp += fLevelUpEquipMaxhp;


        }
    }



    //패널안 레벨업 버튼
    public void EquipLevelUpButton()
    {
        Item item = Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex];
        if (MenuData.Instance.fCurrentShooter > 0 && item.fLevelShootButton <= MenuData.Instance.fCurrentShooter)
        {
            
            MenuData.Instance.fCurrentShooter -= item.fLevelShootButton;
            LevelUpShooterMinus(Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex]);
            
            


            SoundManager.Instance.ButtonSound("ButtonClick", clip);
        }
        else
        {
           if(MenuData.Instance.fCurrentShooter < 0 || item.fLevelShootButton > MenuData.Instance.fCurrentShooter)
            {
                NotEnoughtMoneyPeanl[Inventory.Instance.selectedSlotIndex].SetActive(true);


                SoundManager.Instance.ButtonSound("ButtonSound", clip);
            }
        }

    }

    //패널 버튼 끄기

    public void NotEnoughCheckButton()
    {
        NotEnoughtMoneyPeanl[Inventory.Instance.selectedSlotIndex].SetActive(false);
    }


    public void EquipTextCollecting(Item item)
    {
        
        //사진은 인벤토리랑 인벤토리 매니저에 배치 참고할것.
        //장비 이름
        
        EquipNameText[Inventory.Instance.selectedSlotIndex].text = "" + item.itemName;
   
        //장비 등급
        EquipGradeText[Inventory.Instance.selectedSlotIndex].text = "" + item.ItemGrade;

        //장비레벨
        EquipLevelPenalText[Inventory.Instance.selectedSlotIndex].text  = "레벨" + item.quantity + "/" + "" + item.Maxquantity;

        //장비메인설명
        EquipDescriptionText[Inventory.Instance.selectedSlotIndex].text = "" + item.Description;

        //속성
        EquipInformationText[Inventory.Instance.selectedSlotIndex].text = "속성";

        //장비부연설명
        EquipSubDescriptionText[Inventory.Instance.selectedSlotIndex].text = "" + item.SubDescription;

        //데미지 
        EquipDamageText[Inventory.Instance.selectedSlotIndex].text = "공격" + "+" + item.fDamage;

        //레벨업시 추가 데미지
        EquipLevelUpPlusDamageText[Inventory.Instance.selectedSlotIndex].text = "(+" + ""+ item.fLevelUpPlusDamage + ")";

        //업그레이드 재료
        EquipUpdageMaterialText[Inventory.Instance.selectedSlotIndex].text = "업그레이드 재료";

        EquipWeaponMaterialText[Inventory.Instance.selectedSlotIndex].text = "무기 스크롤" + ":";

        //Hp관련
        EquipHpText[Inventory.Instance.selectedSlotIndex].text = "체력" + "+"  + item.fHp;

        EquipLevelUpHpText[Inventory.Instance.selectedSlotIndex].text = "(+" + "" +  item.fLevelUpPlusHp + ")";





        //레벌업 버튼을 위해 가지고 있는 고유값들 -> 따로 InventoryManager에서 참고할것. 
        EquipButtonLevelUpText[Inventory.Instance.selectedSlotIndex].text = "x" + "" + item.fLevelShootButton  + "\n레벨업";
  
    } 

    
    public void UpdateUI()
    {
        for (int i = 0; i < PenalSlots.Count; i++)
        {
            PeanlSlot slot = PenalSlots[i].GetComponent<PeanlSlot>();
            if (i < PenalSlots.Count)
            {
                slot.AddItem(Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex]);

            }
            else
            {
                slot.CelarSlot();
            }
        }
    }

}
