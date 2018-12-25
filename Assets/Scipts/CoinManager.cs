﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour {
    public static CoinManager instance;
    public int coin;
    public Text coinText;

    private void Start()
    {
        if (instance == null) instance = this;
        coin = 600;
    }
    public void addCoin(int amount)
    {
        coin += amount;
        UpdateCoin();
    }
    public void reduceCoin(int amount)
    {
        coin -= amount;
        UpdateCoin();
    }
    public bool requestCoin(int amount)
    {
        if (amount <= coin) return true;
        return false;
    }
    public void UpdateCoin()
    {
        coinText.text = coin.ToString();
    }
}
