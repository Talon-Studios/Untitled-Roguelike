using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrades
{
    FasterMovement,
    FireFaster,
    MoreDamage,
    FasterBullets,
    MoreSpread,
    AddProjectile
}

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private UpgradeObject[] upgrades;

    [Header("UI")]
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private UpgradeCard[] upgradeCards;

    [Header("Upgrade Options")]
    [SerializeField] private float speedPercentIncrease = 20;
    [SerializeField] private float fireRatePercentIncrease = 20;
    [SerializeField] private float damagePercentIncrease = 20;
    [SerializeField] private float bulletSpeedPercentIncrease = 20;
    [SerializeField] private float spreadPercentIncrease = 20;

    #region Singleton
    
    static public UpgradeManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void SetupUpgradesPanel() {
        upgradesPanel.SetActive(true);
        Time.timeScale = 0;
        
        UpgradeObject[] randomUpgrades = GetRandomUpgrades(3).ToArray();
        for (int i = 0; i < randomUpgrades.Length; i++)
        {
            UpgradeObject randomUpgrade = randomUpgrades[i];

            upgradeCards[i].titleText.text = randomUpgrade.upgradeName;
            upgradeCards[i].descriptionText.text = randomUpgrade.upgradeDescription;
            upgradeCards[i].upgrade = randomUpgrade;
        }
    }

    public void CloseUpgradesPanel() {
        upgradesPanel.SetActive(false);
        MousePointer.Instance.SetMouseState(MouseState.Aiming);
        Time.timeScale = 1;
    }

    public void ActivateUpgrade(UpgradeObject upgradeObject) {
        switch (upgradeObject.upgrade)
        {
            case Upgrades.FasterMovement: { 
                PlayerMovement.Instance.speed += PercentOf(PlayerMovement.Instance.speed, speedPercentIncrease);
                break;
            }
            case Upgrades.FireFaster: {
                PlayerShooting.Instance.fireRate += PercentOf(PlayerShooting.Instance.fireRate, fireRatePercentIncrease);
                break;
            }
            case Upgrades.MoreDamage: {
                PlayerShooting.Instance.damage += PercentOf(PlayerShooting.Instance.damage, damagePercentIncrease);
                break;
            }
            case Upgrades.FasterBullets: {
                PlayerShooting.Instance.bulletSpeed += PercentOf(PlayerShooting.Instance.bulletSpeed, bulletSpeedPercentIncrease);
                break;
            }
            case Upgrades.MoreSpread: {
                PlayerShooting.Instance.spread += PercentOf(PlayerShooting.Instance.spread, spreadPercentIncrease);
                break;
            }
            case Upgrades.AddProjectile: {
                PlayerShooting.Instance.projectiles++;
                break;
            }
        }
    }

    private List<UpgradeObject> GetRandomUpgrades(int amount) {
        List<UpgradeObject> upgradesPossible = new List<UpgradeObject>(upgrades);
        List<UpgradeObject> upgradeResults = new List<UpgradeObject>();
        
        for (int i = 0; i < amount; i++)
        {
            UpgradeObject randomUpgrade = upgradesPossible[Random.Range(0, upgradesPossible.Count)];
            upgradesPossible.Remove(randomUpgrade);
            upgradeResults.Add(randomUpgrade);
        }

        return upgradeResults;
    }

    private float PercentOf(float number, float percentage) {
        return number / 100 * percentage;
    }

}
