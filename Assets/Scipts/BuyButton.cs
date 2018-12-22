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
    private void Start()
    {
        if (instance != null) instance = this;
    }
    // mua item
    public void _toBuyItem()
    {
        if (itemID == 0)
        {
            Debug.Log("Error");
            return;
        }

        for (int i = 0; i < shopManager.itemList.Count; i++)
        {
            // check id
            if (itemID == shopManager.itemList[i].itemID)
            {
                // check bought
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
                 //   SaveLoad.instance.savingStar_2();
                }
            }
            else
            {

            }
        }
    }
    // xu ly mua
    public void _buyingProcess()
    {
        int i = itemID;
        int requestCoin = shopManager.itemList[i].itemCoin;
        if (shopManager.coinManager.requestCoin(requestCoin))
        {
            shopManager.itemList[i].bought = true;
            PlayerPrefs.SetInt("currentItemID", shopManager.itemList[i].itemID);
            shopManager.coinManager.reduceCoin(requestCoin);
            shopManager.boughtList.Add(shopManager.itemList[i]);
            shopManager.currentItemID = itemID;
            shopManager.UpdateUI();
            Destroy(Instantiate(effectBuyingItem, shopManager.itemObjectList[i].transform));
            //SaveLoad.instance.saving();
            //SaveLoad.instance.savingStar_2();

        }
        else
        {
        }       
    }
}
