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
    public int currentItemID = 0;
    public List<Sprite> imageItemList = new List<Sprite>();
    public Transform container;
    public GameObject prefItem;
    int count;
    private void Start()
    {
        count = itemList.Count;
        currentItemID = PlayerPrefs.GetInt("currentItemID");
        Debug.Log("currentItemID");
        if (instance == null) instance = this;

        //Load data
       // SaveLoad.instance.loading();
       // SaveLoad.instance.loadingStar_2();

        // setup ui
        if (itemList != null)
        {
            itemList[0].bought = true;
            boughtList.Add(itemList[0]);
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefItem, container, false);

            // xet da mua
            if (itemList[i].bought)
            {
                // snake chua su dung
                if (itemList[i].itemID != currentItemID)
                {
                }
                // snake da mua va su dung
                else
                {

                }

            }
            else
            {

            }
            itemObjectList.Add(obj);
            snap.btnn.Add(obj.GetComponent<Button>());
        }
        coinManager.UpdateCoin();
        UpdateUI();
    }
    // update UI
    public void UpdateUI()
    {
        PlayerPrefs.SetInt("currentID", currentItemID);
        for (int i = 0; i < boughtList.Count; i++)
        {
            for (int j = 0; j < itemList.Count; j++)
            {

                if (itemList[j].bought == boughtList[i].bought)
                {
                    // snake item da mua nhung k su dung
                    if (itemList[j].itemID != currentItemID)
                    {
                    }
                    // snake da mua va su dung
                    else
                    {
                    }
                }
            }
        }
    }
}
