using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrades
{
    FasterMovement,
    FireFaster,
    MoreDamage,
    FasterBullets
}

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private UpgradeObject[] upgrades;

    [Header("UI")]
    [SerializeField] private GameObject upgradesPanel;

    // [Header("Upgrade Options")]

    public void SetupUpgradesPanel() {
        upgradesPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseUpgradesPanel() {
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ActivateUpgrade(UpgradeObject upgradeObject) {
        switch (upgradeObject.upgrade)
        {
            case Upgrades.FasterMovement: { 
                PlayerMovement.Instance.speed = PercentOf(PlayerMovement.Instance.speed, 120);
                break;
            }
            case Upgrades.FireFaster: {
                PlayerShooting.Instance.fireRate = PercentOf(PlayerShooting.Instance.fireRate, 150);
                break;
            }
            case Upgrades.MoreDamage: {
                PlayerShooting.Instance.damage = PercentOf(PlayerShooting.Instance.damage, 120);
                break;
            }
            case Upgrades.FasterBullets: {
                PlayerShooting.Instance.bulletSpeed = PercentOf(PlayerShooting.Instance.damage, 120);
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
