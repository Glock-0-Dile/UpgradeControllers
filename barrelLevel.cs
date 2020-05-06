using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barrelLevel : MonoBehaviour
{
    public weaponUpgradeHandler weaponUpgradeHandler;
    public moneyUpdater moneyUpdater;
    public Text text;
    public float ammoDamageMultiplier;
    public float AccuracyMultiplier;
    public float currentMoney;
    public float cost;
    public float purchased;
    
    void Start()
    {
        text.text = "Price: " + cost.ToString();
    }
    public void onclick()
    {
         moneyUpdater.askForMoney();
        
        if(currentMoney >= cost && (purchased == 0))
        {
            currentMoney -= cost;
            weaponUpgradeHandler.barrel(ammoDamageMultiplier, AccuracyMultiplier);
            moneyUpdater.updateMoney(currentMoney);
            text.text = "Purchased";
            purchased = 1;
        
        }

        if(purchased == 1)
        {
            weaponUpgradeHandler.barrel(ammoDamageMultiplier, AccuracyMultiplier);
            text.text = "Equiped";
        }
    }

    public void updateMoney(float money)
    {
        currentMoney = money;
    }
}
