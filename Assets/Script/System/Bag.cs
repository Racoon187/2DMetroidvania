using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public int CoinNum;
    public GameObject myBag;
    bool isOpen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
        CoinNum = Coin.MyCoin;
    }

    private void OpenMyBag()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;  //∑¥œÚ—≠ª∑
            myBag.SetActive(isOpen);
        }
    }
}
