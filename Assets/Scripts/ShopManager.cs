using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public CoinManager coinManager;
    public Snap snap;

    public List<GameObject> itemObjectList = new List<GameObject>();
    public List<Item> itemList = new List<Item>();
    public List<Item> boughtList = new List<Item>();
    public List<Sprite> imageItemList = new List<Sprite>();

    public int count;
    public int currentItemID = 0;
    public Transform container;
    public GameObject prefItem;

    // update UI
    public virtual void UpdateUI()
    {
    }
    public virtual void _buyItem(int i)
    {
        int requestCoin = itemList[i].itemCoin;
        if (coinManager.requestCoin(requestCoin))
        {
            itemList[i].bought = true;
            PlayerPrefs.SetInt("currentItemID", itemList[i].itemID);
            coinManager.reduceCoin(requestCoin);
            boughtList.Add(itemList[i]);
            currentItemID = i;
            UpdateUI();
            //Destroy(Instantiate(effectBuyingItem, shopManager.itemObjectList[i].transform));
            SaveLoad.instance.saving(this);
            Debug.Log("curret buy: " + currentItemID);
        }
        else
        {
        }
    }
    public virtual void _updateItem(int itemID)
    {
        PlayerPrefs.SetInt("currentItemID", itemID);
        currentItemID = itemID;
        UpdateUI();
        SaveLoad.instance.saving(this);
        Debug.Log("curret update: " + currentItemID);
    }
}

