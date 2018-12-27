using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CarShop : ShopManager
{
    private Color unBought = new Color(243f / 255, 184f / 255, 95f / 255, 255f / 255);
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        count = itemList.Count;
        currentItemID = PlayerPrefs.GetInt("currentItemID");
        Debug.Log("currentItemID");
        if (instance == null) instance = this;

        //Load data
        SaveLoad.instance.loading(this);

        // setup ui
        if (itemList != null)
        {
            itemList[0].bought = true;
            boughtList.Add(itemList[0]);
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefItem, container, false);
            itemObjectList.Add(obj);
            snap.btnn.Add(obj.GetComponent<Button>());
            // xet da mua
            if (itemList[i].bought)
            {
                itemObjectList[i].transform.GetChild(0).GetComponent<Image>().material = null;
                itemObjectList[i].transform.GetChild(0).GetComponent<Image>().color
                     = new Color(1, 1, 1, 1);
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
        }
        coinManager.UpdateCoin();
        UpdateUI();
        snap._setupStart();
    }

    // update UI
    override
    public void UpdateUI()
    {
        PlayerPrefs.SetInt("currentItemID", currentItemID);
        for (int i = 0; i < boughtList.Count; i++)
        {
            for (int j = 0; j < itemList.Count; j++)
            {

                if (itemList[j].bought == boughtList[i].bought)
                {
                    itemObjectList[j].transform.GetChild(0).GetComponent<Image>().material = null;
                    itemObjectList[j].transform.GetChild(0).GetComponent<Image>().color
                         = new Color(1, 1, 1, 1);
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

