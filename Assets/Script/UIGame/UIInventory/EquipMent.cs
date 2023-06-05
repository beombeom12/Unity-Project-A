using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EquipMent : MonoBehaviour
{
    public static EquipMent Instance
    {
        get
        {
            if (equipmentInstance == null)
            {
                equipmentInstance = FindObjectOfType<EquipMent>();
                if (equipmentInstance == null)
                {
                    var instanceContainer = new GameObject("Equipment");
                    equipmentInstance = instanceContainer.AddComponent<EquipMent>();
                }
            }
            return equipmentInstance;
        }
    }
    private static EquipMent equipmentInstance;





    //부모형식
    public GameObject EquipmnentPenal;
        //프리팹으로 만든 슬롯
    public GameObject EquipmentPrefab;


    public List<Item> ItemList = new List<Item>();
    public List<GameObject> EquipmentSlots = new List<GameObject>();


    public const int EquipmentCount = 6;



 

    private void Start()
    {

        for(int i =0; i < EquipmentCount; i++)
        {
            GameObject slot = Instantiate(EquipmentPrefab, EquipmnentPenal.transform);
            EquipmentSlots.Add(slot);
          
        }
    }

    public void AddItem(Item item)
    {


        if (item.itemType == ItemType.Weapon)
        {
            EquipmentSlot slot = EquipmentSlots[0].GetComponent<EquipmentSlot>();
            slot.AddItem(item);
          
        }
        else if (item.itemType == ItemType.Armor)
        {
            EquipmentSlot slot = EquipmentSlots[1].GetComponent<EquipmentSlot>();
            slot.AddItem(item);

        }
        else if (item.itemType == ItemType.RightRing)
        {
            EquipmentSlot slot = EquipmentSlots[2].GetComponent<EquipmentSlot>();
            slot.AddItem(item);
        }
        else if (item.itemType == ItemType.LeftRing)
        {
            EquipmentSlot slot = EquipmentSlots[3].GetComponent<EquipmentSlot>();
            slot.AddItem(item);
        }
        else if(item.itemType == ItemType.RightPet)
        {
            EquipmentSlot slot = EquipmentSlots[4].GetComponent<EquipmentSlot>();

            slot.AddItem(item);
        }
        else if(item.itemType == ItemType.LeftPet)
        {
            EquipmentSlot slot = EquipmentSlots[5].GetComponent<EquipmentSlot>();
            slot.AddItem(item);
        }

    }




     void UpdateUI()
    {
        for(int i = 0; i < EquipmentSlots.Count; i++)
        {
            EquipmentSlot slot = EquipmentSlots[i].GetComponent<EquipmentSlot>();

            if(i < ItemList.Count)
            {
                slot.AddItem(ItemList[i]);
            }
            else
            {
                slot.ClearSlot();
            }
        }
    }



}