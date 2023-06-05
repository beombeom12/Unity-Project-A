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



   
    
    
    // 인벤토리 UI 요소
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject[] slotPenals;



    // 인벤토리 아이템 리스트
    public List<Item> itemList = new List<Item>();
    public List<GameObject> slotList = new List<GameObject>();





    public List<GameObject> ItemEquip = new List<GameObject>();
    // 인벤토리 슬롯 개수
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
        // 아이템 추가
        ItemDatabase itemDatabase = ItemDatabase.Instance;


        //방어구
        //황금 흉갑

        itemList.Add(itemDatabase.CreateArmorItem("황금흉갑", Resources.Load<Sprite>("20001"), 20001, "에픽", 1, 20,
        "순금으로 만든 보호구, 바위처럼 단단하죠.", "튼튼함", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.Armor));
        //손재주의 조끼
        itemList.Add(itemDatabase.CreateArmorItem("손재주의 조끼", Resources.Load<Sprite>("20002"), 20002, "에픽", 1, 20,
         "가볍고 내구성이 있어, 적의 공격을\n피하고 싶다면 필수입니다.", "날렵함", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.Rob));
        //팬덤망토 
        itemList.Add(itemDatabase.CreateArmorItem("팬텀망토", Resources.Load<Sprite>("20003"), 20003, "레어", 1, 20,
        "아주 멋진 망토, 원거리 발사체로 인한 \n데미지를 줄여줍니다.", "닥터스트레인지", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.Doctorst));
        // 보이드로브 
        itemList.Add(itemDatabase.CreateArmorItem("보이드로브", Resources.Load<Sprite>("20004"), 20003, "에픽", 1, 20,
      "아주 멋진 망토, 원거리 발사체로 인한 \n데미지를 줄여줍니다.", "닥터스트레인지", 0f, 0f, 50f, 50f, 0f, 0f, 200f, ArmorType.AncientRob));




        //무기
        itemList.Add(itemDatabase.CreateWeaponItem("용감한활", Resources.Load<Sprite>("10001"), 10001,"레어", 1,20,
            "자격을 지닌 자만이 엄청난 속도와 성능의 활을 \n쏠 수 있습니다." ,"균형이 잘잡힌 무기",100f,0f, 0f,0f,0f,0f, 200f, WeaponType.AncientBow));
        itemList.Add(itemDatabase.CreateWeaponItem("활기찬활", Resources.Load<Sprite>("10002"), 10002,"에픽", 1,20, 
            "실력이 좋은 자만이 엄청난 속도의 \n코딩을 칠 수 있습니다.", "밸런스가 잘잡힌 무기", 110f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Bow));
        itemList.Add(itemDatabase.CreateWeaponItem("데스사이드", Resources.Load<Sprite>("10003"), 10003,"레어", 1, 20,
            "강력한 사이드, 매우 무겁고 공격 속도가 \n느립니다.", "속도는 느리지만, 공격력이 높습니다.", 100f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Sickle));
        itemList.Add(itemDatabase.CreateWeaponItem("수어사이드", Resources.Load<Sprite>("10004"), 10004,"에픽", 1,20,
            "한 땀의 의지, 매우 무겁고 코딩 속도가 느립니다.", "속도는 느리지만, 강력합니다!", 110f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.AncientSickle));
        itemList.Add(itemDatabase.CreateWeaponItem("쏘우 블레이드", Resources.Load<Sprite>("10005"), 10005,"레어", 1, 20, 
            "닌자가 쓰는 신비의 무기, 다소 약하나,\n공격 속도가 매우 빠릅니다.", "공격력이 낮지만, 속도는 빠릅니다.", 100f,0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Sword));
        itemList.Add(itemDatabase.CreateWeaponItem("주온 블레이드", Resources.Load<Sprite>("10006"), 10006,"에픽", 1, 20, 
            "사무라이가 쓰는 신비의 무기, 다소 약하나,\n공격 속도가 엄청 빠릅니다.", "공격력이 낮지만, 속도만 빠릅니다.", 110f,0f, 0f, 0f, 0f, 0f, 200f, WeaponType.AncientSword));
        itemList.Add(itemDatabase.CreateWeaponItem("부메랑", Resources.Load<Sprite>("10007"), 10007,"레어", 1,20,
            "레벨업시 날아가다 되돌아오는 날카로운 부메랑", "무기가 나에게 되돌아옵니다.", 100f,0f, 0f, 0f, 0f, 0f, 200f, WeaponType.Commendation));
        itemList.Add(itemDatabase.CreateWeaponItem("굉장한부메랑", Resources.Load<Sprite>("10008"), 10008,"에픽", 1,20, 
            "레벨업시 날아가다 되돌아오는 날카로운 \n굉장한 부메랑", "부메랑이 날아올때 30퍼의 데미지가 \n낮춰집니다.", 110f, 0f, 0f, 0f, 0f, 0f, 200f, WeaponType.AncientCommendation));



        //오른쪽 반지
        //곰반지
        itemList.Add(itemDatabase.CreateRightRingItem("곰돌이 반지", Resources.Load<Sprite>("30001"), 30001,"레어", 1,20,"곰의 힘을 얻게 됩니다.","환웅", 
            10f, 0f, 20f, 20f,0f, 0f, 200f, RightRingType.BearMetalRing));
        //곰반지
        itemList.Add(itemDatabase.CreateRightRingItem("곰 반지", Resources.Load<Sprite>("30002"), 30002, "에픽", 1,20,"곰의 힘을 얻게 됩니다.", "웅녀", 
            10f, 0f, 20f, 20f, 0f, 0f, 200f, RightRingType.BearSilverRing));
        //늑대 반지
        itemList.Add(itemDatabase.CreateRightRingItem("늑대의 반지", Resources.Load<Sprite>("30003"), 30003, "레어", 1, 20, "늑대의 힘을 얻게 됩니다.", "늑대", 
            10f, 0f, 20f, 20f, 0f, 0f, 200f, RightRingType.WolfMetalRing));
        //늑대 반지
        itemList.Add(itemDatabase.CreateRightRingItem("늑대의 반지", Resources.Load<Sprite>("30004"), 30004, "에픽", 1, 20, "늑대의 힘을 얻게 됩니다.", "늑대",
            10f, 0f, 20f, 20f, 0f, 0f, 200f, RightRingType.WolfSilverRing));


        //왼쪽 반지
        itemList.Add(itemDatabase.CreateLeftRingItem("독수리 반지", Resources.Load<Sprite>("40001"), 40001,"레어", 1,20,"독수리의 힘을 얻게 됩니다.","턱돌려깎음",
            10f, 0f, 20f, 20f, 0f, 0f, 200f, LeftRingRype.EagleMetalRing));
        
        itemList.Add(itemDatabase.CreateLeftRingItem("독수리 반지", Resources.Load<Sprite>("40002"), 40002, "에픽", 1, 20, "독수리의 힘을 얻게 됩니다.", "턱돌려깎음",
            13f, 0f, 23f, 23f, 0f, 0f, 200f, LeftRingRype.EagleSilverRing));
        
        itemList.Add(itemDatabase.CreateLeftRingItem("뱀의 반지", Resources.Load<Sprite>("40003"), 40003, "레어", 1, 20, "뱀의 힘을 얻게 됩니다.", "슬렌데린", 
            10f, 0f, 20f, 20f, 0f, 0f, 200f, LeftRingRype.SnakeMetalRing));
        
        itemList.Add(itemDatabase.CreateLeftRingItem("뱀의 반지", Resources.Load<Sprite>("40004"), 40004, "에픽", 1, 20, "뱀의 힘을 얻게 됩니다.", "슬렌데린", 
            13f, 0f, 23f, 23f, 0f, 0f, 200f, LeftRingRype.SnakeSilverRing));



        //오른쪽 펫 
        //요정
        itemList.Add(itemDatabase.CreateRightPetItem("엘프", Resources.Load<Sprite>("50001"), 50001, "에픽", 1, 20, "검은 숲의 엘프, 발사체를 발사합니다.", "발사체를 발사합니다.",
            13f, 0f, 23f, 23f, 0f, 0f, 200f, RightPetType.Angel));
        //사신
        itemList.Add(itemDatabase.CreateRightPetItem("사이드 메이지", Resources.Load<Sprite>("50002"), 50002, "에픽", 1, 20, "죽음 그 자체와도 같은 존재. 그의\n사이드는 적을 간단히 베어버립니다.", "사이드가 적을 베어버립니다.", 
            13f, 0f, 23f, 23f, 0f, 0f, 200f, RightPetType.Reaper));


        //왼쪽 펫
        //귀신
        itemList.Add(itemDatabase.CreateLeftPetItem("리빙 봄", Resources.Load<Sprite>("60001"), 60001, "독특", 1, 20, "영역 데미지를 가하는 폭탄을 계속\n던집니다.", "공격을 둔화시킵니다.영역 데미지.", 
            13f, 0f, 23f, 23f, 0f, 0f, 200f, LeftPetType.Ghost));
        //박쥐
        itemList.Add(itemDatabase.CreateLeftPetItem("외눈의 박쥐", Resources.Load<Sprite>("60002"), 60002, "독특", 1, 20, "영역 데미지를 가하는 폭탄을 계속\n던집니다.", "공격을 둔화시킵니다.영역 데미지.", 
            13f, 0f, 23f,23f, 0f, 0f, 200f, LeftPetType.Bat));



     




        // 인벤토리 슬롯 생성
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


    // 인벤토리에 아이템 추가
    public void AddItem(Item item)
    {
        // 인벤토리가 가득 차지 않은 경우
        if (itemList.Count < slotCount)
        {
            itemList.Add(item);
            UpdateUI();
        }

    }

    // 인벤토리 UI 갱신
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

    //장비 장착.
    //무기 
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
             
                //흉갑이 있따면 -> 황금흉갑을 장착 시키고 -> 
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

        //갑옷
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

    //오른쪽반지
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
            //       //만약 첫번째 캐릭터에서 -> 활 등등이 다 껴져있다면
            //if(slotPenals[12].activeSelf == true && EquipItem)


            //반지첫번째
            if (slotPenals[12].activeSelf == true)
            {
            
                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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







            //반지두번째
            if (slotPenals[13].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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


            //반지 세번째

            if (slotPenals[14].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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


            //반지 네번째
            if (slotPenals[15].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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
    //왼쪽반지
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
            //반지첫번째
            if (slotPenals[16].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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







            //반지두번째
            if (slotPenals[17].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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


            //반지 세번째

            if (slotPenals[18].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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


            //반지 네번째
            if (slotPenals[19].activeSelf == true)
            {

                //0번째옷
                //////slots  0 slots1 1번 임.
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


                //1번옷

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



                //2번째옷
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


                //3번째옷
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




                //두번째옷
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



                //세번째옷
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

               
                
                //네번째옷
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




                //두번째옷
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



                //세번째옷
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



                //세번째옷
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




                //두번째옷
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



                //세번째옷
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



                //네번째옷
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




                //두번째옷
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



                //세번째옷
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



                //세번째옷
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