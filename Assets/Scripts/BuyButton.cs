﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{
    public int itemID;
    public ShopManager shopManager;
    public GameObject effectBuyingItem;
    public Text state; // Unlock or lock
    public Text itemCoinText;
    private void Start()
    {
        itemID = shopManager.snap.getMinButtonNum();
    }
    private void Update()
    {
        if (!shopManager.snap.getDrag())
        {
            itemID = shopManager.snap.getMinButtonNum();
            int i = getIndexOfItem();
            if (itemID == -1)
            {
                Debug.Log("Error");
                return;
            }
            checkShow(i);
        }
    }
    // mua item
    public void _toBuyItem()
    {
        int i = getIndexOfItem();
        if (itemID == -1)
        {
            Debug.Log("Error");
            return;
        }
        // check bought
        checkBought(i);
    }

    // update Item
    public void Oke()
    {
        if (shopManager.itemList[getIndexOfItem()].bought){
            _updateUI(itemID);
        }
    }

    // Show BuyButton (Sate, Coin)
    public void checkShow(int i)
    {
        itemCoinText.text = shopManager.itemList[i].itemCoin.ToString();
        if (!shopManager.itemList[i].bought)
        {
            gameObject.GetComponent<Image>().enabled = true;
            Transform childs = gameObject.transform;
            int count = gameObject.transform.childCount;
            if (count == 0)
                return;
            for (int j = 0; j < count; j++)
            {
                childs.GetChild(j).gameObject.SetActive(true);
            }
            state.text = "UNLOCK";
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
            Transform childs = gameObject.transform;
            int count = gameObject.transform.childCount;
            if (count == 0)
                return;
            for (int j = 0; j < count; j++)
            {
                childs.GetChild(j).gameObject.SetActive(false);
            }
        }
    }
    // Check if chua mua thi xu ly, da mua roi thi update
    public void checkBought(int i)
    {
        if (!shopManager.itemList[i].bought)
        {
            _buyingProcess(itemID);
        }
        else
        {
            _updateUI(itemID);
            //   SaveLoad.instance.saving();
        }
    }
    // lay vi tri cua item da pick
    public int getIndexOfItem()
    {
        for (int i = 0; i < shopManager.itemList.Count; i++)
        {
            // check id
            if (itemID == shopManager.itemList[i].itemID)
            {
                return i;
            }
        }
        return 0;
    }
    // xu ly mua
    void _buyingProcess(int _itemID)
    {
        shopManager._buyItem(_itemID);
        shopManager.saving();
    }

    void _updateUI(int _itemID)
    {
        shopManager._updateItem(_itemID);
        shopManager.saving();
    }
    
}
