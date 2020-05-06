using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponUpgradeHandler : MonoBehaviour
{
    public float damageMultiplier;
    public float accuracyMultiplier;
    public float magazine;
    public float sight;
    public float stock;
    public float ammo;
    public float reciever;

    public newPlayerController playerController;
    public magazineLevel magazineLevel;
    public magazineLevel magazineLevel2;
    public magazineLevel magazineLevel3;
    public magazineLevel magazineLevel4;
    public recieverLevel recieverLevel;
    public recieverLevel recieverLevel2;
    public recieverLevel recieverLevel3;
    public recieverLevel recieverLevel4;

    public void barrel(float ammoDamage, float accuracy)
    {
        damageMultiplier = ammoDamage;
        accuracyMultiplier = accuracy;
        playerController.barrelMultiplierUpdate(damageMultiplier);
        playerController.accuracyMultiplierUpdate(accuracyMultiplier);
    }

    public void magazineUpdate(float count)
    {
        magazine = count;
        playerController.magSizeUpdate(magazine);
    }

    public void recieverUpdate(float multi)
    {
        reciever = multi;
        playerController.recieverUpdate(reciever);
    }

    public void magChanged()
    {
        magazineLevel.setPurchased();
        magazineLevel2.setPurchased();
        magazineLevel3.setPurchased();
        magazineLevel4.setPurchased();

    }

    public void recieverChanged()
    {
        recieverLevel.setPurchased();
        recieverLevel2.setPurchased();
        recieverLevel3.setPurchased();
        recieverLevel4.setPurchased();
    }
}
