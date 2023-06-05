using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{


    public static EquipmentSlot Instance
    {
        get
        {
            if (EquipmentInstance == null)
            {
                EquipmentInstance = FindObjectOfType<EquipmentSlot>();
                if (EquipmentInstance == null)
                {
                    var InstanceContainer = new GameObject("EquipmentSlot");
                    EquipmentInstance = InstanceContainer.AddComponent<EquipmentSlot>();
                }
            }
            return EquipmentInstance;
        }
    }



    public Image icon;

    public Item item;


    private static EquipmentSlot EquipmentInstance;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
     //   removeButton.interactable = false;
    }



    //장착제거,인벤토리에서 추가가되야하는것이고.
    public void OnRemoveButton()
    {

        if (item != null && item.itemType == ItemType.Weapon)
        {
            WeaponItem weaponItem = (WeaponItem)item;

            item = null;
            icon.sprite = null;
            icon.enabled = false;
            PlayerSkillData.Instance.fDamage -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fDamage;
   

        }
        else if (item != null && item.itemType == ItemType.Armor)
        {
            ArmorItem armorItem = (ArmorItem)item;

            item = null;
            icon.sprite = null;
            icon.enabled = false;
            PlayerSkillData.Instance.fPlayerCurrentHp -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp;
            PlayerSkillData.Instance.fPlayerMaxHp -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp;


        }
        else if (item != null && item.itemType == ItemType.RightRing)
        {
            RightRingItem rightringitem = (RightRingItem)item;
            rightringitem = null;
            icon.sprite = null;
            icon.enabled = false;
            PlayerSkillData.Instance.fDamage -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fDamage;
            PlayerSkillData.Instance.fPlayerCurrentHp -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp;
            PlayerSkillData.Instance.fPlayerMaxHp -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp;
        }
        else if (item != null && item.itemType == ItemType.LeftRing)
        {
            LeftRingItem leftringitem = (LeftRingItem)item;
            leftringitem = null;
            icon.sprite = null;
            icon.enabled = false;
            PlayerSkillData.Instance.fDamage -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fDamage;
            PlayerSkillData.Instance.fPlayerCurrentHp -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp;
            PlayerSkillData.Instance.fPlayerMaxHp -= Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex].fHp;
        }
        else if(item != null && item.itemType == ItemType.RightPet)
        {
            RightPet rightpet = (RightPet)item;
            rightpet = null;
            icon.sprite = null;
            icon.enabled = false;
        }
        else if(item != null && item.itemType == ItemType.LeftPet)
        {
            LeftPet leftpet = (LeftPet)item;
            leftpet = null;
            icon.sprite = null;
            icon.enabled = false;
        }
        


    }


 

   
}