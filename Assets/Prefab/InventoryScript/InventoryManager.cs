using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory playerBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public TextMeshProUGUI itemInfo;
    public TextMeshProUGUI CoinNum;

    void Awake()
    {
        if (instance == null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInfo.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInfo.text = itemDescription;
    }

    public static void CreateNewItem(Item item)  //背包传输
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemNum.ToString();
    }

    public static void RefreshItem()  //销毁&重新创建物品
    {
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < instance.playerBag.itemList.Count; i++)
        {
            CreateNewItem(instance.playerBag.itemList[i]);
        }
    }
}
