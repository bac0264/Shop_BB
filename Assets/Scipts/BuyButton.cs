using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{

    public static BuyButton instance;
    public int itemID;
    public ShopManager shopManager;
    public GameObject effectBuyingItem;
    public Text state; // Unlock or lock
    public Text itemCoinText;
    private void Start()
    {
        if (instance != null) instance = this;
        itemID = shopManager.snap.getMinButtonNum();
        Debug.Log("itemID: " + itemID);
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
            _buyingProcess();
        }
        else
        {
            PlayerPrefs.SetInt("currentID", itemID);
            shopManager.currentItemID = itemID;
            shopManager.UpdateUI();
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
    public void _buyingProcess()
    {
        int i = itemID;
        //int requestCoin = shopManager.itemList[i].itemCoin;
        //if (shopManager.coinManager.requestCoin(requestCoin))
        //{
        //    shopManager.itemList[i].bought = true;
        //    PlayerPrefs.SetInt("currentItemID", shopManager.itemList[i].itemID);
        //    shopManager.coinManager.reduceCoin(requestCoin);
        //    shopManager.boughtList.Add(shopManager.itemList[i]);
        //    shopManager.currentItemID = itemID;
        //    shopManager.UpdateUI();
        //    //Destroy(Instantiate(effectBuyingItem, shopManager.itemObjectList[i].transform));
        //    //SaveLoad.instance.saving();
        //    //SaveLoad.instance.savingStar_2();

        //}
        //else
        //{
        //}
        shopManager._buyItem(i);
    }
}
