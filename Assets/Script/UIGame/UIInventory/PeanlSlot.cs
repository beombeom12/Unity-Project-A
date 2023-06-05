using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PeanlSlot : MonoBehaviour
{
    public static PeanlSlot Instance
    {
        get
        {
            if (penalslotInstance == null)
            {
                penalslotInstance = FindObjectOfType<PeanlSlot>();
                if (penalslotInstance == null)
                {
                    var InstanceContainer = new GameObject("PeanlSlot");
                    penalslotInstance = InstanceContainer.AddComponent<PeanlSlot>();
                }
            }
            return penalslotInstance;
        }
    }

    private static PeanlSlot penalslotInstance;

    public Item item;
    public Image icon;
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;

    }

    public void CelarSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }



}
