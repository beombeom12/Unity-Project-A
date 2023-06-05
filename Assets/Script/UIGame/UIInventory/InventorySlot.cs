using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour
{

    public static InventorySlot Instance
    {
        get
        {
            if (InventoryInstance == null)
            {
                InventoryInstance = FindObjectOfType<InventorySlot>();
                if (Instance == null)
                {
                    var InstanceContainer = new GameObject("InventorySlot");
                    InventoryInstance = InstanceContainer.AddComponent<InventorySlot>();
                }
            }
            return InventoryInstance;
        }
    }

    private static InventorySlot InventoryInstance;
    public Image iconImage;
    public Item item;



    public GameObject slotPanel;

    public int slotNumber;


    public int index;


    public void Update()
    {
        
    }


    public void ClearSlot()
    {
        item = null;
        iconImage.sprite = null;
        iconImage.enabled = false;

    }
    
    public void AddItem(Item items)
    {
        item = items;
        iconImage.sprite = item.itemIcon;
        iconImage.enabled = true;
    }

    public void OpenPanel()
    {
        if (slotPanel != null)
        {

          
            //Inventory.Instance.UIPlayer.SetActive(false);
            //OnSlotClick();
            InventoryManager.Instance.UpdateUI();
        }

    }


    //인덱스번호 받아오기. -> Inventory에서 선택된 이벤트 발생시킨다. 제발 되라

    public void OnSlotClick()
    {
        Inventory.Instance.selectedSlotIndex = index;



        if (Inventory.Instance.slotPenals[Inventory.Instance.selectedSlotIndex] != null)
        {
            // 슬롯의 아이템이 있을 때 해당 슬롯 번호에 해당하는 패널을 활성화합니다.
            Inventory.Instance.slotPenals[Inventory.Instance.selectedSlotIndex].SetActive(true);
            InventoryManager.Instance.EquipTextCollecting(Inventory.Instance.itemList[Inventory.Instance.selectedSlotIndex]);

            for(int i = 0; i < 13; i++)
            {
                Inventory.Instance.EquipItem[i].SetActive(false);
            }

            Inventory.Instance.UIPlayerPosition.SetActive(false);

            InventoryManager.Instance.GetInfoPenalSlot();
            // 업데이트를 수행합니다.



        }
    }

}