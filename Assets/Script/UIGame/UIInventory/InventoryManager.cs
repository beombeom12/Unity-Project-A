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

    //���Ÿ��ҽ� ���� �г�
    public GameObject[] NotEnoughtMoneyPeanl;
    //������ ���� �θ������հ� �ڽ� ������
    public GameObject[] ParentPenalPC;
    public GameObject PenalPCPrefabs;

    public List<GameObject> PenalSlots = new List<GameObject>();

    
    //Text Collect

    public Text PlayerDamageText;
    public Text PlayerHpText;

    //Equipment for Penal Description;
    //�̸�
    public Text[] EquipNameText;
    //����
    public Text[] EquipLevelPenalText;
    //���
    public Text[] EquipGradeText;
    //����
    public Text[] EquipDescriptionText;
    //�Ӽ�
    public Text[] EquipInformationText;
    //�ο�����
    public Text[] EquipSubDescriptionText;
    //������
    public Text[] EquipDamageText;
    //���������߰�������
    public Text[] EquipLevelUpPlusDamageText;

    //�Ʒ� �Ӽ� ���⽺ũ����.
    //���׷��̵� ���
    public Text[] EquipUpdageMaterialText;
    //���⽺ũ��.
    public Text[] EquipWeaponMaterialText;
    //�гξ� ������ ��ư ������ 20 ������ Ŀ���� ��������.
    public Text[] EquipButtonLevelUpText;
    //HP���õȰ� 
    public Text[] EquipHpText;
    public Text[] EquipLevelUpHpText;


    //�������� ��ư ����
    private float fLevelUpPlusing = 200f;
    
    //�������� ������ ��������
    private float fEquipDamage = 30f;
    private float fLevelUpEuqipDamage = 3f;

    //�������� ������ HP
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
        //���â���� �÷��̾� ���� �ҷ�����
        UpdateUI();
        PlayerInformationInfo();
        EquipTextCollecting(Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex]);
       
    }


    //����г��� ���� ��ư 
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

    //���â�� �÷��̾� ������ HP���� �ҷ����� 
    public void PlayerInformationInfo()
    {


        PlayerDamageText.text = "" + PlayerSkillData.Instance.fDamage;

        PlayerHpText.text = "" + PlayerSkillData.Instance.fPlayerMaxHp;
    }


    //�������� ���Ͱ� �����°��� �ι辿 �ø���.
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



    //�гξ� ������ ��ư
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

    //�г� ��ư ����

    public void NotEnoughCheckButton()
    {
        NotEnoughtMoneyPeanl[Inventory.Instance.selectedSlotIndex].SetActive(false);
    }


    public void EquipTextCollecting(Item item)
    {
        
        //������ �κ��丮�� �κ��丮 �Ŵ����� ��ġ �����Ұ�.
        //��� �̸�
        
        EquipNameText[Inventory.Instance.selectedSlotIndex].text = "" + item.itemName;
   
        //��� ���
        EquipGradeText[Inventory.Instance.selectedSlotIndex].text = "" + item.ItemGrade;

        //��񷹺�
        EquipLevelPenalText[Inventory.Instance.selectedSlotIndex].text  = "����" + item.quantity + "/" + "" + item.Maxquantity;

        //�����μ���
        EquipDescriptionText[Inventory.Instance.selectedSlotIndex].text = "" + item.Description;

        //�Ӽ�
        EquipInformationText[Inventory.Instance.selectedSlotIndex].text = "�Ӽ�";

        //���ο�����
        EquipSubDescriptionText[Inventory.Instance.selectedSlotIndex].text = "" + item.SubDescription;

        //������ 
        EquipDamageText[Inventory.Instance.selectedSlotIndex].text = "����" + "+" + item.fDamage;

        //�������� �߰� ������
        EquipLevelUpPlusDamageText[Inventory.Instance.selectedSlotIndex].text = "(+" + ""+ item.fLevelUpPlusDamage + ")";

        //���׷��̵� ���
        EquipUpdageMaterialText[Inventory.Instance.selectedSlotIndex].text = "���׷��̵� ���";

        EquipWeaponMaterialText[Inventory.Instance.selectedSlotIndex].text = "���� ��ũ��" + ":";

        //Hp����
        EquipHpText[Inventory.Instance.selectedSlotIndex].text = "ü��" + "+"  + item.fHp;

        EquipLevelUpHpText[Inventory.Instance.selectedSlotIndex].text = "(+" + "" +  item.fLevelUpPlusHp + ")";





        //������ ��ư�� ���� ������ �ִ� �������� -> ���� InventoryManager���� �����Ұ�. 
        EquipButtonLevelUpText[Inventory.Instance.selectedSlotIndex].text = "x" + "" + item.fLevelShootButton  + "\n������";
  
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
