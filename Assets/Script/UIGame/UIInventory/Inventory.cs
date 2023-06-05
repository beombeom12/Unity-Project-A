using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour
{
    public static Inventory Instance
    {
        get
        {
            if (InventoryInstance == null)
            {
                InventoryInstance = FindObjectOfType<Inventory>();
                if (InventoryInstance == null)
                {
                    var InstanceContainer = new GameObject("Inventory");
                    InventoryInstance = InstanceContainer.AddComponent<Inventory>();
                }
            }
            return InventoryInstance;
        }
    }
    private static Inventory InventoryInstance;



   
    
    
    // �κ��丮 UI ���
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject[] slotPenals;



    // �κ��丮 ������ ����Ʈ
    public List<Item> itemList = new List<Item>();
    public List<GameObject> slotList = new List<GameObject>();





    public List<GameObject> ItemEquip = new List<GameObject>();
    // �κ��丮 ���� ����
    public const  int slotCount = 24;

    public int selectedSlotIndex { get; set; }

    public AudioClip clip;



    //test
    public Animator anim;





    //UIPlayer Controll
    public GameObject UIPlayer;
    public GameObject UIBasePlayer;
    public GameObject UIPlayerPosition;

    public List<GameObject> EquipItem = new List<GameObject>();




    //public void Awake()
    //{
    //    if (InventoryInstance == null)
    //    {
    //        InventoryInstance = this;
    //        DontDestroyOnLoad(InventoryInstance);
    //        SceneManager.sceneLoaded += OnSceneLoaded;

    //    }
    //    else
    //    {
    //        Destroy(gameObject);

    //    }
    //}
    //private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{

    //}

    private void Start()
    {
        // ������ �߰�
        ItemDatabase itemDatabase = ItemDatabase.Instance;


        //��
        //Ȳ�� �䰩

        itemList.Add(itemDatabase.CreateArmorItem("Ȳ���䰩", Resources.Load<Sprite>("20001"), 20001, "����", 1, 20,
        "�������� ���� ��ȣ��, ����ó�� �ܴ�����.", "ưư��", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.Armor));
        //�������� ����
        itemList.Add(itemDatabase.CreateArmorItem("�������� ����", Resources.Load<Sprite>("20002"), 20002, "����", 1, 20,
         "������ �������� �־�, ���� ������\n���ϰ� �ʹٸ� �ʼ��Դϴ�.", "������", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.Rob));
        //�Ҵ����� 
        itemList.Add(itemDatabase.CreateArmorItem("���Ҹ���", Resources.Load<Sprite>("20003"), 20003, "����", 1, 20,
        "���� ���� ����, ���Ÿ� �߻�ü�� ���� \n�������� �ٿ��ݴϴ�.", "���ͽ�Ʈ������", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.Doctorst));
        // ���̵�κ� 
        itemList.Add(itemDatabase.CreateArmorItem("���̵�κ�", Resources.Load<Sprite>("20004"), 20003, "����", 1, 20,
      "���� ���� ����, ���Ÿ� �߻�ü�� ���� \n�������� �ٿ��ݴϴ�.", "���ͽ�Ʈ������", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.AncientRob));




        //����
        itemList.Add(itemDatabase.CreateWeaponItem("�밨��Ȱ", Resources.Load<Sprite>("10001"), 10001,"����", 1,20,
            "�ڰ��� ���� �ڸ��� ��û�� �ӵ��� ������ Ȱ�� \n�� �� �ֽ��ϴ�." ,"������ ������ ����",100f,0f, 0f,0f,0f,0f, 200f, WeaponType.AncientBow));
        itemList.Add(itemDatabase.CreateWeaponItem("Ȱ����Ȱ", Resources.Load<Sprite>("10002"), 10002,"����", 1,20, 
            "�Ƿ��� ���� �ڸ��� ��û�� �ӵ��� \n�ڵ��� ĥ �� �ֽ��ϴ�.", "�뷱���� ������ ����", 110f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Bow));
        itemList.Add(itemDatabase.CreateWeaponItem("�������̵�", Resources.Load<Sprite>("10003"), 10003,"����", 1, 20,
            "������ ���̵�, �ſ� ���̰� ���� �ӵ��� \n�����ϴ�.", "�ӵ��� ��������, ���ݷ��� �����ϴ�.", 100f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Sickle));
        itemList.Add(itemDatabase.CreateWeaponItem("������̵�", Resources.Load<Sprite>("10004"), 10004,"����", 1,20,
            "�� ���� ����, �ſ� ���̰� �ڵ� �ӵ��� �����ϴ�.", "�ӵ��� ��������, �����մϴ�!", 110f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.AncientSickle));
        itemList.Add(itemDatabase.CreateWeaponItem("��� ���̵�", Resources.Load<Sprite>("10005"), 10005,"����", 1, 20, 
            "���ڰ� ���� �ź��� ����, �ټ� ���ϳ�,\n���� �ӵ��� �ſ� �����ϴ�.", "���ݷ��� ������, �ӵ��� �����ϴ�.", 100f,0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Sword));
        itemList.Add(itemDatabase.CreateWeaponItem("�ֿ� ���̵�", Resources.Load<Sprite>("10006"), 10006,"����", 1, 20, 
            "�繫���̰� ���� �ź��� ����, �ټ� ���ϳ�,\n���� �ӵ��� ��û �����ϴ�.", "���ݷ��� ������, �ӵ��� �����ϴ�.", 110f,0f, 0f, 0f, 0f, 0f, 200f, WeaponType.AncientSword));
        itemList.Add(itemDatabase.CreateWeaponItem("�θ޶�", Resources.Load<Sprite>("10007"), 10007,"����", 1,20,
            "�������� ���ư��� �ǵ��ƿ��� ��ī�ο� �θ޶�", "���Ⱑ ������ �ǵ��ƿɴϴ�.", 100f,0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Commendation));
        itemList.Add(itemDatabase.CreateWeaponItem("�����Ѻθ޶�", Resources.Load<Sprite>("10008"), 10008,"����", 1,20, 
            "�������� ���ư��� �ǵ��ƿ��� ��ī�ο� \n������ �θ޶�", "�θ޶��� ���ƿö� 30���� �������� \n�������ϴ�.", 110f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.AncientCommendation));



        //������ ����
        //������
        itemList.Add(itemDatabase.CreateRightRingItem("������ ����", Resources.Load<Sprite>("30001"), 30001,"����", 1,20,"���� ���� ��� �˴ϴ�.","ȯ��", 
            10f, 0f, 20f, 20f,0f, 0f, 200f, RightRingType.BearMetalRing));
        //������
        itemList.Add(itemDatabase.CreateRightRingItem("�� ����", Resources.Load<Sprite>("30002"), 30002, "����", 1,20,"���� ���� ��� �˴ϴ�.", "����", 
            10f, 0f, 20f, 20f, 0f, 0f, 200f, RightRingType.BearSilverRing));
        //���� ����
        itemList.Add(itemDatabase.CreateRightRingItem("������ ����", Resources.Load<Sprite>("30003"), 30003, "����", 1, 20, "������ ���� ��� �˴ϴ�.", "����", 
            10f, 0f, 20f, 20f, 0f, 0f, 200f, RightRingType.WolfMetalRing));
        //���� ����
        itemList.Add(itemDatabase.CreateRightRingItem("������ ����", Resources.Load<Sprite>("30004"), 30004, "����", 1, 20, "������ ���� ��� �˴ϴ�.", "����",
            10f, 0f, 20f, 20f, 0f, 0f, 200f, RightRingType.WolfSilverRing));


        //���� ����
        itemList.Add(itemDatabase.CreateLeftRingItem("������ ����", Resources.Load<Sprite>("40001"), 40001,"����", 1,20,"�������� ���� ��� �˴ϴ�.","�ε�������",
            10f, 0f, 20f, 20f, 0f, 0f, 200f, LeftRingRype.EagleMetalRing));
        
        itemList.Add(itemDatabase.CreateLeftRingItem("������ ����", Resources.Load<Sprite>("40002"), 40002, "����", 1, 20, "�������� ���� ��� �˴ϴ�.", "�ε�������",
            13f, 0f, 23f, 23f, 0f, 0f, 200f, LeftRingRype.EagleSilverRing));
        
        itemList.Add(itemDatabase.CreateLeftRingItem("���� ����", Resources.Load<Sprite>("40003"), 40003, "����", 1, 20, "���� ���� ��� �˴ϴ�.", "��������", 
            10f, 0f, 20f, 20f, 0f, 0f, 200f, LeftRingRype.SnakeMetalRing));
        
        itemList.Add(itemDatabase.CreateLeftRingItem("���� ����", Resources.Load<Sprite>("40004"), 40004, "����", 1, 20, "���� ���� ��� �˴ϴ�.", "��������", 
            13f, 0f, 23f, 23f, 0f, 0f, 200f, LeftRingRype.SnakeSilverRing));



        //������ �� 
        //����
        itemList.Add(itemDatabase.CreateRightPetItem("����", Resources.Load<Sprite>("50001"), 50001, "����", 1, 20, "���� ���� ����, �߻�ü�� �߻��մϴ�.", "�߻�ü�� �߻��մϴ�.",
            13f, 0f, 23f, 23f, 0f, 0f, 200f, RightPetType.Angel));
        //���
        itemList.Add(itemDatabase.CreateRightPetItem("���̵� ������", Resources.Load<Sprite>("50002"), 50002, "����", 1, 20, "���� �� ��ü�͵� ���� ����. ����\n���̵�� ���� ������ ��������ϴ�.", "���̵尡 ���� ��������ϴ�.", 
            13f, 0f, 23f, 23f, 0f, 0f, 200f, RightPetType.Reaper));


        //���� ��
        //�ͽ�
        itemList.Add(itemDatabase.CreateLeftPetItem("���� ��", Resources.Load<Sprite>("60001"), 60001, "��Ư", 1, 20, "���� �������� ���ϴ� ��ź�� ���\n�����ϴ�.", "������ ��ȭ��ŵ�ϴ�.���� ������.", 
            13f, 0f, 23f, 23f, 0f, 0f, 200f, LeftPetType.Ghost));
        //����
        itemList.Add(itemDatabase.CreateLeftPetItem("�ܴ��� ����", Resources.Load<Sprite>("60002"), 60002, "��Ư", 1, 20, "���� �������� ���ϴ� ��ź�� ���\n�����ϴ�.", "������ ��ȭ��ŵ�ϴ�.���� ������.", 
            13f, 0f, 23f,23f, 0f, 0f, 200f, LeftPetType.Bat));



     




        // �κ��丮 ���� ����
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
            slotList.Add(slot);

            InventorySlot slotScripts = slot.GetComponent<InventorySlot>();
            slotScripts.slotPanel = slotPenals[i];
   
            slotScripts.index = i; 

            if (i < itemList.Count)
            {
                InventorySlot slotScript = slot.GetComponent<InventorySlot>();
                slotScript.AddItem(itemList[i]);
            }


        }





        anim = GetComponent<Animator>();


    }

    public void Update()
    {
        UpdateUI();

    }


    // �κ��丮�� ������ �߰�
    public void AddItem(Item item)
    {
        // �κ��丮�� ���� ���� ���� ���
        if (itemList.Count < slotCount)
        {
            itemList.Add(item);
            UpdateUI();
        }

    }

    // �κ��丮 UI ����
    public void UpdateUI()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            InventorySlot slot = slotList[i].GetComponent<InventorySlot>();
            if (i < itemList.Count)
            {
                slot.AddItem(itemList[i]);
            }
            else
            {
                slot.ClearSlot();
            }
        }
    }

    //��� ����.
    //���� 
    public void EquipInventory()
    {

      
        Item item = itemList[selectedSlotIndex];
        PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage - IntroScript.Instance.fWeaponPlusDamage;
        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[1].GetComponent<EquipmentSlot>();
        if (item != null && item.itemType == ItemType.Weapon)
        {
            EquipMent.Instance.AddItem(item);
            Instantiate(UIEffect.Instance.UIPlayerEquipWeaponEffect, new Vector3(UIPlayer.transform.position.x + 0.1f , UIPlayer.transform.position.y - 0.5931852f, UIPlayer.transform.position.z - 35f), Quaternion.Euler(-15f, 0f, 0f));
           
            PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage +  itemList[selectedSlotIndex].fDamage + itemList[selectedSlotIndex].fLevelUpPlusDamage
                + ButtonController.Instance.fPower + ButtonController.Instance.fAllPower;
            SoundManager.Instance.ButtonSound("ButtonClick", clip);

            UIPlayerPosition.SetActive(true);

            if (slotPenals[4].activeSelf == true && EquipItem[5].activeSelf == false)
            {   //else if (slots.icon.sprite = itemList[8].itemIcon)
             
                //�䰩�� �ֵ��� -> Ȳ���䰩�� ���� ��Ű�� -> 
                    if (slots.icon.sprite == itemList[0].itemIcon)
                    {
                        EquipItem[1].SetActive(true);
                        EquipItem[5].SetActive(true);
                        EquipItem[6].SetActive(false);
                        EquipItem[7].SetActive(false);
                        EquipItem[8].SetActive(false);
                        EquipItem[9].SetActive(false);
                        EquipItem[10].SetActive(false);
                        EquipItem[11].SetActive(false);
                        EquipItem[12].SetActive(false);
                    }
                    else if(slots.icon.sprite == itemList[1].itemIcon)
                    {
                      
                      EquipItem[1].SetActive(false);
                      EquipItem[2].SetActive(true);
                      EquipItem[5].SetActive(true);
                      EquipItem[6].SetActive(false);
                      EquipItem[7].SetActive(false);
                      EquipItem[8].SetActive(false);
                      EquipItem[9].SetActive(false);
                      EquipItem[10].SetActive(false);
                      EquipItem[11].SetActive(false);
                      EquipItem[12].SetActive(false);
                    }
                     else if (slots.icon.sprite == itemList[2].itemIcon)
                     {
                         EquipItem[3].SetActive(true);
                         EquipItem[5].SetActive(true);
                         EquipItem[6].SetActive(false);
                         EquipItem[7].SetActive(false);
                         EquipItem[8].SetActive(false);
                         EquipItem[9].SetActive(false);
                         EquipItem[10].SetActive(false);
                         EquipItem[11].SetActive(false);
                         EquipItem[12].SetActive(false);
                     }
                     else if (slots.icon.sprite == itemList[3].itemIcon)
                     {
                         EquipItem[4].SetActive(true);
                         EquipItem[5].SetActive(true);
                         EquipItem[6].SetActive(false);
                         EquipItem[7].SetActive(false);
                         EquipItem[8].SetActive(false);
                         EquipItem[9].SetActive(false);
                         EquipItem[10].SetActive(false);
                         EquipItem[11].SetActive(false);
                         EquipItem[12].SetActive(false);
                     }

            }
            else if (slotPenals[5].activeSelf == true && EquipItem[6].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(true);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(true);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(true);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(true);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }

            }
            else if (slotPenals[6].activeSelf == true && EquipItem[7].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(true);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(true);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(true);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(true);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
            }
            else if (slotPenals[7].activeSelf == true && EquipItem[8].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(true);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(true);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(true);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(true);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
            }
            else if (slotPenals[8].activeSelf == true && EquipItem[9].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(true);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(true);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(true);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(true);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
            }
            else if (slotPenals[9].activeSelf == true && EquipItem[10].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(true);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(true);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(true);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(true);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(false);
                }
            }
            else if (slotPenals[10].activeSelf == true && EquipItem[11].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(true);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(true);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(true);
                    EquipItem[12].SetActive(false);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(true);
                    EquipItem[12].SetActive(false);
                }
            }
            else if (slotPenals[11].activeSelf == true && EquipItem[12].activeSelf == false)
            {
                if (slots.icon.sprite == itemList[0].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[1].itemIcon)
                {

                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[2].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[3].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(false);
                    EquipItem[6].SetActive(false);
                    EquipItem[7].SetActive(false);
                    EquipItem[8].SetActive(false);
                    EquipItem[9].SetActive(false);
                    EquipItem[10].SetActive(false);
                    EquipItem[11].SetActive(false);
                    EquipItem[12].SetActive(true);
                }
            }
            slotPenals[selectedSlotIndex].SetActive(false);

            UpdateUI();
        }
 
         

    }

        //����
    public void EquipArmorInventory()
    {
        
        Item itemtoArmor = itemList[selectedSlotIndex];
        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[0].GetComponent<EquipmentSlot>();
        //PlayerSkillData.Instance.fPlayerCurrentHp = 1000f;
        //PlayerSkillData.Instance.fPlayerMaxHp = 1000f;
        PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp - IntroScript.Instance.fArmorPlusMaxHp;
        PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp - IntroScript.Instance.fArmorPlusHp;
        if (itemtoArmor.itemType == ItemType.Armor)
        {
            EquipMent.Instance.AddItem(itemtoArmor);
            Instantiate(UIEffect.Instance.UIPlayerEquipArmorEffect, new Vector3(UIPlayer.transform.position.x + 0.1f, UIPlayer.transform.position.y - 0.5931852f, UIPlayer.transform.position.z - 35f), Quaternion.Euler(-15f, 0f, 0f));
            PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp +  itemList[selectedSlotIndex].fMaxHp + itemList[selectedSlotIndex].fLevelUpPlusMaxHp + ButtonController.Instance.fMaxHp + ButtonController.Instance.fAllMaxHp;
            PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp + itemList[selectedSlotIndex].fHp + itemList[selectedSlotIndex].fLevelUpPlusHp + ButtonController.Instance.fHp + ButtonController.Instance.fAllHp;
            SoundManager.Instance.ButtonSound("ButtonClick", clip);
            UIPlayerPosition.SetActive(true);
            if (slotPenals[0].activeSelf == true && EquipItem[1].activeSelf == false)
            {
                EquipItem[0].SetActive(false);
                EquipItem[1].SetActive(true);
                EquipItem[2].SetActive(false);
                EquipItem[3].SetActive(false);
                EquipItem[4].SetActive(false);
                if (slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[5].SetActive(true);

                }
                else if(slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[6].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[7].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[8].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[9].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[10].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[11].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(true);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[12].SetActive(true);

                }
            }
            else if (slotPenals[1].activeSelf == true && EquipItem[2].activeSelf == false)
            {
                EquipItem[0].SetActive(false);
                EquipItem[1].SetActive(false);
                EquipItem[2].SetActive(true);
                EquipItem[3].SetActive(false);
                EquipItem[4].SetActive(false);
                if (slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[5].SetActive(true);

                }
                else if (slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[6].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[7].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[8].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[9].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[10].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[11].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(true);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(false);
                    EquipItem[12].SetActive(true);
                }
            }
            else if (slotPenals[2].activeSelf == true && EquipItem[3].activeSelf == false)
            {
                EquipItem[0].SetActive(false);
                EquipItem[1].SetActive(false);
                EquipItem[2].SetActive(false);
                EquipItem[3].SetActive(true);
                EquipItem[4].SetActive(false);
                if (slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[5].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[6].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[7].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[8].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[9].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[10].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[11].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(true);
                    EquipItem[4].SetActive(false);
                    EquipItem[12].SetActive(true);
                }

            }
            else if (slotPenals[3].activeSelf == true && EquipItem[4].activeSelf == false)
            {
                EquipItem[0].SetActive(false);
                EquipItem[1].SetActive(false);
                EquipItem[2].SetActive(false);
                EquipItem[3].SetActive(false);
                EquipItem[4].SetActive(true);
                if (slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);
                }
                else if(slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[0].SetActive(false);
                    EquipItem[1].SetActive(false);
                    EquipItem[2].SetActive(false);
                    EquipItem[3].SetActive(false);
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }

            }



            slotPenals[selectedSlotIndex].SetActive(false);
            UpdateUI();
        }
    
    }

    //�����ʹ���
    public void EquipRightRingInventory()
    {
        Item itemToRightRing = itemList[selectedSlotIndex];

        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[0].GetComponent<EquipmentSlot>();
        EquipmentSlot slots1 = EquipMent.Instance.EquipmentSlots[1].GetComponent<EquipmentSlot>();

        PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage - IntroScript.Instance.fRightRingDamage;
        PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp - IntroScript.Instance.fRightRingMaxHp;
        PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp - IntroScript.Instance.fRightRingHp;
        if (itemToRightRing.itemType == ItemType.RightRing)
        {
            EquipMent.Instance.AddItem(itemToRightRing);
            Instantiate(UIEffect.Instance.UIPlayerEquipRightRingEffect, new Vector3(UIPlayer.transform.position.x + 0.1f, UIPlayer.transform.position.y - 0.5931852f, UIPlayer.transform.position.z - 35f), Quaternion.Euler(-15f, 0f, 0f));
            PlayerSkillData.Instance.fDamage = + PlayerSkillData.Instance.fDamage +  itemList[selectedSlotIndex].fDamage + itemList[selectedSlotIndex].fLevelUpPlusDamage;
            PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp +  itemList[selectedSlotIndex].fMaxHp + itemList[selectedSlotIndex].fLevelUpPlusMaxHp ;
            PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp +  itemList[selectedSlotIndex].fHp + itemList[selectedSlotIndex].fLevelUpPlusHp ;
            SoundManager.Instance.ButtonSound("ButtonClick", clip);
            UIPlayerPosition.SetActive(true);
            //         // else if (slotPenals[3].activeSelf == true && EquipItem[4].activeSelf == false)
            //       //���� ù��° ĳ���Ϳ��� -> Ȱ ����� �� �����ִٸ�
            //if(slotPenals[12].activeSelf == true && EquipItem)


            //����ù��°
            if (slotPenals[12].activeSelf == true)
            {
            
                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }

            }







            //�����ι�°
            if (slotPenals[13].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }
            }


            //���� ����°

            if (slotPenals[14].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }
            }


            //���� �׹�°
            if (slotPenals[15].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }
            }
            slotPenals[selectedSlotIndex].SetActive(false);

            UpdateUI();
        }
    }
    //���ʹ���
    public void EquipLeftRingInventory()
    {
    

        Item itemToLeftRing = itemList[selectedSlotIndex];
        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[0].GetComponent<EquipmentSlot>();
        EquipmentSlot slots1 = EquipMent.Instance.EquipmentSlots[1].GetComponent<EquipmentSlot>();

        PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage - IntroScript.Instance.fLeftRingDamage;
        PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp - IntroScript.Instance.fLeftRingMaxHp;
        PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp - IntroScript.Instance.fLeftRingHp;

        if (itemToLeftRing.itemType == ItemType.LeftRing)
        {
            EquipMent.Instance.AddItem(itemToLeftRing);


            Instantiate(UIEffect.Instance.UIPlayerEquipLeftRingEffct, new Vector3(UIPlayer.transform.position.x + 0.1f, UIPlayer.transform.position.y - 0.5931852f, UIPlayer.transform.position.z - 35f), Quaternion.Euler(-15f, 0f, 0f));
            PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage + itemList[selectedSlotIndex].fDamage + itemList[selectedSlotIndex].fLevelUpPlusDamage;
            PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp + itemList[selectedSlotIndex].fMaxHp + itemList[selectedSlotIndex].fLevelUpPlusMaxHp;
            PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp + itemList[selectedSlotIndex].fHp + itemList[selectedSlotIndex].fLevelUpPlusHp;

            SoundManager.Instance.ButtonSound("ButtonClick", clip);
            UIPlayerPosition.SetActive(true);
            //����ù��°
            if (slotPenals[16].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }

            }







            //�����ι�°
            if (slotPenals[17].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }
            }


            //���� ����°

            if (slotPenals[18].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }
            }


            //���� �׹�°
            if (slotPenals[19].activeSelf == true)
            {

                //0��°��
                //////slots  0 slots1 1�� ��.
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //1����

                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                }



                //2��°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                }


                //3��°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);

                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                }
            }

            slotPenals[selectedSlotIndex].SetActive(false);
            UpdateUI();
        }
    }


    public void EquipRightPetitemInventory()
    {



        Item itemToRightPet = itemList[selectedSlotIndex];
        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[0].GetComponent<EquipmentSlot>();
        EquipmentSlot slots1 = EquipMent.Instance.EquipmentSlots[1].GetComponent<EquipmentSlot>();
        PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage - IntroScript.Instance.fFirstPetDamage;
        PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp - IntroScript.Instance.fFirstPetMaxHp;
        PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp - IntroScript.Instance.fFirstPetHp;
        if (itemToRightPet.itemType == ItemType.RightPet)
        {
            EquipMent.Instance.AddItem(itemToRightPet);
            Instantiate(UIEffect.Instance.UIPlayerEquipRightPetEffect, new Vector3(UIPlayer.transform.position.x + 0.1f, UIPlayer.transform.position.y - 0.5931852f, UIPlayer.transform.position.z - 35f), Quaternion.Euler(-15f, 0f, 0f));
            PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage + itemList[selectedSlotIndex].fDamage + itemList[selectedSlotIndex].fLevelUpPlusDamage ;

            PlayerSkillData.Instance.fPlayerMaxHp =PlayerSkillData.Instance.fPlayerMaxHp + itemList[selectedSlotIndex].fMaxHp + itemList[selectedSlotIndex].fLevelUpPlusMaxHp ;
            PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp +  itemList[selectedSlotIndex].fHp + itemList[selectedSlotIndex].fLevelUpPlusHp;
            SoundManager.Instance.ButtonSound("ButtonClick", clip);

            UIPlayerPosition.SetActive(true);
            if (slotPenals[20].activeSelf == true)
            {
                if(slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }




                //�ι�°��
                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }



                //����°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }

               
                
                //�׹�°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[13].SetActive(true);
                    EquipItem[14].SetActive(false);
                }
            }



            if (slotPenals[21].activeSelf == true)
            {
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }




                //�ι�°��
                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }



                //����°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }



                //����°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[14].SetActive(true);
                    EquipItem[13].SetActive(false);
                }
            }










            slotPenals[selectedSlotIndex].SetActive(false);
            UpdateUI();
        }
    }

    public void EquipLeftPetitemInvetory()
    {

        Item itemToLeftPet = itemList[selectedSlotIndex];

        EquipmentSlot slots = EquipMent.Instance.EquipmentSlots[0].GetComponent<EquipmentSlot>();
        EquipmentSlot slots1 = EquipMent.Instance.EquipmentSlots[1].GetComponent<EquipmentSlot>();

        PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage - IntroScript.Instance.fSecondPetDamage;
        PlayerSkillData.Instance.fPlayerMaxHp = PlayerSkillData.Instance.fPlayerMaxHp - IntroScript.Instance.fSecondPetMaxHp;
        PlayerSkillData.Instance.fPlayerCurrentHp = PlayerSkillData.Instance.fPlayerCurrentHp - IntroScript.Instance.fSecondPetHp;


        if (itemToLeftPet.itemType == ItemType.LeftPet)
        {
            EquipMent.Instance.AddItem(itemToLeftPet);
            Instantiate(UIEffect.Instance.UIPlayerEquipLeftPetEffect, new Vector3(UIPlayer.transform.position.x + 0.1f, UIPlayer.transform.position.y - 0.5931852f, UIPlayer.transform.position.z - 35f), Quaternion.Euler(-15f, 0f, 0f));
            SoundManager.Instance.ButtonSound("ButtonClick", clip);
            PlayerSkillData.Instance.fDamage = PlayerSkillData.Instance.fDamage  +  itemList[selectedSlotIndex].fDamage + itemList[selectedSlotIndex].fLevelUpPlusDamage  ;
            PlayerSkillData.Instance.fPlayerMaxHp = itemList[selectedSlotIndex].fMaxHp + PlayerSkillData.Instance.fPlayerMaxHp + itemList[selectedSlotIndex].fLevelUpPlusMaxHp ;
            PlayerSkillData.Instance.fPlayerCurrentHp =  PlayerSkillData.Instance.fPlayerCurrentHp + itemList[selectedSlotIndex].fHp +itemList[selectedSlotIndex].fLevelUpPlusHp;

            UIPlayerPosition.SetActive(true);
            if (slotPenals[22].activeSelf == true)
            {
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }




                //�ι�°��
                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }



                //����°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }



                //�׹�°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[15].SetActive(true);
                    EquipItem[16].SetActive(false);
                }
            }



            if (slotPenals[23].activeSelf == true)
            {
                if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[0].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[1].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }




                //�ι�°��
                if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[1].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[2].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }



                //����°��
                if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[2].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[3].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }



                //����°��
                if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[4].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[5].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[5].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[6].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[6].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[7].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[7].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[8].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[8].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[9].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[9].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[10].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }

                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[10].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[11].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
                else if (slots1.icon.sprite == itemList[3].itemIcon && slots.icon.sprite == itemList[11].itemIcon)
                {
                    EquipItem[4].SetActive(true);
                    EquipItem[12].SetActive(true);
                    EquipItem[16].SetActive(true);
                    EquipItem[15].SetActive(false);
                }
            }




            slotPenals[selectedSlotIndex].SetActive(false);
            UpdateUI();

        }
    }


 

}