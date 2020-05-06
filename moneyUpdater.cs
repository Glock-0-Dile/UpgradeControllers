using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyUpdater : MonoBehaviour
{

    public purchaseHandler purchaseHandler; 
    public respawnPurchaseHandler respawnPurchaseHandler;
    public attackPurchaseHandler attackPurchaseHandler;
    public bonusPurchaseHandler bonusPurchaseHandler;
    public damageLarge damageLarge;
    public barrelLevel barrelLevel;
    public barrelLevel barrelLevel2;
    public barrelLevel barrelLevel3;
    public barrelLevel barrelLevel4;
    public magazineLevel magazineLevel;
    public magazineLevel magazineLevel2;
    public magazineLevel magazineLevel3;
    public magazineLevel magazineLevel4; 


    public Text money;
    public Text storeMoney;
    public float currentMoney;
    public float baseMoney;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void rewardGiven(float changeTo)
    {
        currentMoney += changeTo;
        updateMoney(currentMoney);
    }
    public void updateMoney(float addToMoney)
    {
        currentMoney = addToMoney;
        money.text = currentMoney.ToString();
        purchaseHandler.updateCurrentMoney(currentMoney);
        respawnPurchaseHandler.updateCurrentMoney(currentMoney);
        attackPurchaseHandler.updateCurrentMoney(currentMoney);
        bonusPurchaseHandler.updateCurrentMoney(currentMoney);
        damageLarge.updateMoney(currentMoney);
        storeMoney.text = currentMoney.ToString();
    }

    public void askForMoney()
    {
        damageLarge.updateMoney(currentMoney);
        barrelLevel.updateMoney(currentMoney);
        barrelLevel2.updateMoney(currentMoney);
        barrelLevel3.updateMoney(currentMoney);
        barrelLevel4.updateMoney(currentMoney);
        magazineLevel.updateMoney(currentMoney);
        magazineLevel2.updateMoney(currentMoney);
        magazineLevel3.updateMoney(currentMoney);
        magazineLevel4.updateMoney(currentMoney);
        storeMoney.text = currentMoney.ToString();
    }

    public void chargeMoney(float newCurrent)
    {
        currentMoney = newCurrent;
        updateMoney(newCurrent);
        money.text = currentMoney.ToString();
    }
}
