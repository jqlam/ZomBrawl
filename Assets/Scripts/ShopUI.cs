using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public PlayerShooting playerShooting; public PlayerHealth playerHealth;
    public int costSMG=200, costShotgun=300, costRifle=300, costHeavy=400, costHeal=100; public int healAmount=30;

    void Start(){ if(!playerShooting) playerShooting = FindObjectOfType<PlayerShooting>(); if(!playerHealth) playerHealth = FindObjectOfType<PlayerHealth>(); }
    public void BuyStarter(){ playerShooting.SetWeapon(WeaponType.Starter); }
    public void BuySMG(){ if(GameController.Instance.SpendCoins(costSMG)) playerShooting.SetWeapon(WeaponType.SMG); }
    public void BuyShotgun(){ if(GameController.Instance.SpendCoins(costShotgun)) playerShooting.SetWeapon(WeaponType.Shotgun); }
    public void BuyRifle(){ if(GameController.Instance.SpendCoins(costRifle)) playerShooting.SetWeapon(WeaponType.Rifle); }
    public void BuyHeavy(){ if(GameController.Instance.SpendCoins(costHeavy)) playerShooting.SetWeapon(WeaponType.Heavy); }
    public void BuyHeal(){ if(GameController.Instance.SpendCoins(costHeal)) playerHealth.Heal(healAmount); }
}
