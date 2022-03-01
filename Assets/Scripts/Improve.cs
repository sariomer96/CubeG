using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Improve : MonoBehaviour
{
    [SerializeField] int _healthMoney=10;
    [SerializeField] Text _healthMoneyTxt;
    [SerializeField] Button _healthMButton;

    [SerializeField] int _earningMoney=10;
    [SerializeField] Text _earningMoneyTxt;
    [SerializeField] Button _earnMButton;
    // Start is called before the first frame update
    void Start()
    {
        _healthMButton.interactable = false;
        _earnMButton.interactable = false;
        PlayerPrefs.SetInt("money", 150);
        CoinStatus._instance.MoneyToString();
        PlayerPrefs.SetInt("healthMoney", 10);
        PlayerPrefs.SetInt("earningMoney", 10);
        int healthMoney = PlayerPrefs.GetInt("healthMoney",10);
        int earnMoney = PlayerPrefs.GetInt("earningMoney",10);
        ButtonInteractable(earnMoney, _earnMButton);
        ButtonInteractable(healthMoney, _healthMButton);

        SetText();

   
    }
    void SetText()
    {
        _healthMoneyTxt.text = PlayerPrefs.GetInt("healthMoney",10).ToString();
        _earningMoneyTxt.text = PlayerPrefs.GetInt("earningMoney",10).ToString();
    }
    void ButtonInteractable(int value,Button improveBtn)
    {
        int totalMoney = PlayerPrefs.GetInt("money", 0);
        if (totalMoney>=value)
        {
            improveBtn.interactable = true;
        }
        else
        {
            improveBtn.interactable = false;
        }
    }
  

    public void BuyImproving(string prefsString)
    {
       
       int totalMoney= PlayerPrefs.GetInt("money", 0);
        totalMoney -= PlayerPrefs.GetInt(prefsString,10);
        print(totalMoney);
        PlayerPrefs.SetInt("money", totalMoney);
        CoinStatus._instance.MoneyToString();

      

        int improveValue = PlayerPrefs.GetInt(prefsString, 10);
        improveValue += 10;
        PlayerPrefs.SetInt(prefsString, improveValue);
        SetText();
        ButtonInteractable(PlayerPrefs.GetInt(prefsString), _healthMButton);
        ButtonInteractable(PlayerPrefs.GetInt(prefsString), _earnMButton);

    }

    public void HealthIncrease()
    {
       int health= PlayerPrefs.GetInt("health",3);
        health++;
        PlayerPrefs.SetInt("health", health);
        CharacterCollisions._instance.Health = health;
        CharacterCollisions._instance._healthTxt.text = health.ToString();
    }
    public void DiamondsValueIncrease()
    {
        int diaBlue=PlayerPrefs.GetInt("diaBlue", 1);
        diaBlue *= 2;
        int diaYellow=PlayerPrefs.GetInt("diaYellow", 2);
        diaYellow *= 2;

        PlayerPrefs.SetInt("diaBlue", diaBlue);
        PlayerPrefs.SetInt("diaYellow", diaYellow);
    }
    private void Update()
    {
      
    }

}
