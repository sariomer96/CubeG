using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinStatus : MonoBehaviour
{
    public Text moneyTxt;
    public string totalCoin= "TOTAL COIN: ";
    private int _money=0;
    [SerializeField] public int Money { get { return _money; } set { _money = value; } }
    public static CoinStatus _instance;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        int totalMoney= PlayerPrefs.GetInt("money",0);

        moneyTxt.text= totalCoin + totalMoney.ToString();
    }


}
