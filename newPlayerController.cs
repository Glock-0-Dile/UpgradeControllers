using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlayerController : MonoBehaviour
{
    //NEED TO BE SET IN THE EDITOR FOR STUFF TO WORK
    #region basePlayerVariables
    //Base health is used for the players health, Will be increased with leveling up
    public float baseHealth;
    //Used in calculation for barrel Damage and ammo damage
    public float baseDamage;
    //Time between respawns. May be shortened with upgrades later? May be removed totally
    public float baseRespawnDelay;
    //Time between reloads
    public float baseReloadDelay;
    //Base attack Delay will act as a fire rate, Being shortened with reviever upgrades
    public float baseAttackDelay;
    //Self explanitory. Will be increased base on magazine bought. 10,15,30,45,50,60,100,120
    public float magazineCapacity;
    public float baseAccuracy;
    #endregion

    #region globalVariables
    public float currentHealth;
    public float toRespawn;
    public float toAttack;
    public float toReload;
    public float damage;
    public float currentAmmo;
    #endregion

    #region weapon Parts
    //The reduction to fire delay given by the reciever. Calculated in its own script when buying
    public float recieverReduction;
    //Static damage based on ammo, Calculated in its own script when buying
    public float ammoDamage;
    //Damage multiplier from barrel, Calculated in its own script when buying
    public float barrelDamageMultiplier;
    public float accuracyMultiplier;
    #endregion

    //Temp Public
    #region private checks
    //           0 = NO, 1 = YES
    public float playerAlive;
    public float enemyAlive;
    public float isPaused;
    #endregion
    
    #region GameObjects
    public newEnemyController enemyController;
    public playerHealthBar playerHealthBar;
    public playerShootBar playerShootBar;
    public playerRespawnBar playerRespawnBar;
    public playerMagBar playerMagBar;
    public damageText damageText;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        playerHealthBar.setMaxHealth(baseHealth);
        playerRespawnBar.setMaxRespawnTime(baseRespawnDelay);
        playerShootBar.setMaxShootTime(baseAttackDelay);
        playerMagBar.setMaxMagCapacity(magazineCapacity);
        playerRespawnBar.setMaxRespawnTime(baseRespawnDelay);
        playerRespawnBar.setRespawnTime(baseRespawnDelay);
        currentAmmo = magazineCapacity;
        currentHealth = baseHealth;
        toAttack = baseAttackDelay - (baseAttackDelay * recieverReduction);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPaused == 1)
        {
            calculateDamage();
        }
        if(isPaused == 0 && (enemyAlive == 1))
        {
            //If you have ammo, are alive, enemy is alive, attack cooldown is over, and you are not currently reloading. Shoot
            if(currentAmmo > 0 && (playerAlive == 1) && (enemyAlive == 1) && (toAttack <= 0) && (toReload <= 0))
            {
                damageEnemy();
            }
       
            if(playerAlive == 0)
            {
                calculateRespawnDelay();
                playerShootBar.setShootTime(baseAttackDelay - (baseAttackDelay * recieverReduction));
                if(toRespawn <= 0)
                {
                    revive();
                }
            }

            if(currentAmmo <= 0)
            {
                reload();
            }

            if(toReload >= 0)
            {
                calculateReloadTime();
            }

            if(toAttack >= 0 && (toReload <= 0))
            {
                calculateAttackTime();
            }
       
        }
    }

    public void takeDamage(float recievedDamage)
    {
        currentHealth -= recievedDamage;
        playerHealthBar.setHealth(currentHealth);
        //checks if player is dead
        if(currentHealth <= 0)
        {
            killed();
        }
    }

    public void isAlive(float number)
    {
        enemyAlive = number;
    }

    //takes in all weapon variables and turns them into their damage
    void calculateDamage()
    {
        //1.BarrelModifier 2.base player damage. 3.baseAmmoDamage
        damage = (ammoDamage * barrelDamageMultiplier) + ammoDamage;
        damageText.display(damage);
        damageText.displayAmmo(ammoDamage);
        damageText.displayBarrel(barrelDamageMultiplier * ammoDamage);
        damageText.displayAccuracy(accuracyMultiplier * baseAccuracy);
        damageText.displayMagSize(magazineCapacity);
    }

    //sends damage to the enemy
    void damageEnemy()
    {
        calculateDamage();
        enemyController.takeDamage(damage);
        toAttack = baseAttackDelay - (baseAttackDelay * recieverReduction);
        playerShootBar.setMaxShootTime(baseAttackDelay - (baseAttackDelay * recieverReduction));
        currentAmmo -= 1;
        playerMagBar.setMagCapacity(currentAmmo);
    }

    //sets player dead and sets the respawn timer and updates health bar and position
    void killed()
    {
        playerAlive = 0;
        toRespawn = baseRespawnDelay;
        playerHealthBar.setHealth(0f);
        //Temporary Death
        transform.position = transform.position + new Vector3(0, -0.6f, 0);
        transform.eulerAngles = new Vector3(0, 0, 90);
        enemyController.isAlive(0);
    }

    //sets player alive and updates health and position when the respawn timer is done
    void revive()
    {
        playerAlive = 1;
        currentHealth = baseHealth;
        playerHealthBar.setHealth(currentHealth);
        playerRespawnBar.setRespawnTime(baseRespawnDelay);
        //Temporary revive
        transform.position = transform.position - new Vector3(0, -0.6f, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        enemyController.isAlive(1);
    }

    //sets ammo to full so it does not call reload(); and sets the reload delay 
    void reload()
    {
        currentAmmo = magazineCapacity;
        toReload = baseReloadDelay;
        toAttack = 0;
        playerMagBar.setMagCapacity(currentAmmo);
    }

    void calculateAttackTime()
    {
        toAttack -= 1;
        playerShootBar.setMaxShootTime(baseAttackDelay - (baseAttackDelay * recieverReduction));
        playerShootBar.setShootTime(toAttack);
    }

    void calculateReloadTime()
    {
        toReload -= 1;
        playerShootBar.setMaxShootTime(baseReloadDelay);
        playerShootBar.setShootTime(toReload);
    }

    void calculateRespawnDelay()
    {
        toRespawn -= 1;
        playerRespawnBar.setRespawnTime(toRespawn);
    }

    #region PublicVoids
    
    public void updatePause(float pause)
    {
        isPaused = pause;
    }
    #endregion

    #region weaponUpgradePassthrough
    public void recieverUpdate(float level)
    {
        recieverReduction = level;
    }

    public void magSizeUpdate(float level)
    {
        magazineCapacity = level;
        playerMagBar.setMaxMagCapacity(level);
    }

    public void ammoUpdate(float level)
    {
        ammoDamage = level;
    }

    public void barrelMultiplierUpdate(float level)
    {
        barrelDamageMultiplier = level;
    }

    public void accuracyMultiplierUpdate(float level)
    {
        accuracyMultiplier = level;
    }
    #endregion
}
