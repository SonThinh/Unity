using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    private static Coins instance;
    private int collectedCoins;

    [SerializeField]
    private Text coinTxt;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Text wintext;

    public static Coins Instance {
        get {
            if(instance == null)
            {
                instance = FindObjectOfType<Coins>();
            }
            return instance; }
    }

    public GameObject CoinPrefab { get => coinPrefab;  }
    public int CollectedCoins { get => collectedCoins;
        set
        {
            coinTxt.text = value.ToString();
            this.collectedCoins = value;
            if(coinTxt.text == "3")
            {
                wintext.text = "WIN!";
            }
        }
    }
}
