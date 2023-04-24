using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrades
{
    Fire,
    FasterFireSpawn,
    Ball,
    Piercing,
    Bomb,
    BombFasterSpawn,
    Laser,
    LaserMoreDamage,
    PoisonTrail,
    PoisonTrailLonger
}

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private List<UpgradeObject> upgrades = new List<UpgradeObject>();

    [Header("UI")]
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private UpgradeCard[] upgradeCards;

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
        if (upgradeObject.upgradeChildren.Length > 0)
        {
            upgrades.Remove(upgradeObject);
            foreach (UpgradeObject childUpgrade in upgradeObject.upgradeChildren)
            {
                upgrades.Add(childUpgrade);
            }
        }

        switch (upgradeObject.upgrade)
        {
            case Upgrades.Fire: {
                print("Fire");
                FireWeapon.Instance.ActivateWeapon();
                break;
            }
            case Upgrades.FasterFireSpawn: {
                print("Fire fireballs faster");
                FireWeapon.Instance.FireFaster(25);
                break;
            }
            case Upgrades.Ball: {
                print("Ball");
                BallWeapon.Instance.ActivateWeapon();
                break;
            }
            case Upgrades.Piercing: {
                print("Piercing");
                PlayerShooting.Instance.PiercingBulletChanceIncrease(20);
                break;
            }
            case Upgrades.Bomb: {
                print("Bomb");
                BombWeapon.Instance.ActivateWeapon();
                break;
            }
            case Upgrades.BombFasterSpawn: {
                print("More bomb");
                BombWeapon.Instance.FireFaster(25);
                break;
            }
            case Upgrades.Laser: {
                print("Laser");
                LaserWeapon.Instance.ActivateWeapon();
                break;
            }
            case Upgrades.LaserMoreDamage: {
                print("More laser damage");
                LaserWeapon.Instance.MoreDamage(15);
                break;
            }
            case Upgrades.PoisonTrail: {
                print("Trail");
                TrailWeapon.Instance.ActiateWeapon();
                break;
            }
            case Upgrades.PoisonTrailLonger: {
                print("Longer trail");
                TrailWeapon.Instance.LongerTrail(1);
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
            if (upgrades.Count >= 3) upgradesPossible.Remove(randomUpgrade);
            upgradeResults.Add(randomUpgrade);
        }

        return upgradeResults;
    }

}
