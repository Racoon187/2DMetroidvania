using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public TextMeshProUGUI slotNum;

    public void ItemCheck()
    {
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }
}
