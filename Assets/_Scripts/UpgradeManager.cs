using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    PoisonTrailLonger,
    FreezeBullets,
    Rotating
}

public class UpgradeManager : MonoBehaviour
{

    public int level = 1;

    [SerializeField] private List<UpgradeObject> upgrades = new List<UpgradeObject>();
    [SerializeField] private List<UpgradeObject> onlyUpgrades = new List<UpgradeObject>();

    [Header("UI")]
    [SerializeField] private TMP_Text levelCounterText;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private UpgradeCard[] upgradeCards;

    #region Singleton
    
    static public UpgradeManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public IEnumerator SetupUpgradesPanel() {
        Time.timeScale = 0;

        yield return null;

        upgradesPanel.SetActive(true);
        
        UpgradeObject[] randomUpgrades = GetRandomUpgrades().ToArray();
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
        Time.timeScale = 1;
        
        MousePointer.Instance.SetMouseState(MouseState.Aiming);
        XPManager.Instance.ResetXP();
        NextLevel();
        EnemyManager.Instance.CheckEnemyTypes();
    }

    public void ActivateUpgrade(UpgradeObject upgradeObject) {
        if (upgradeObject.upgradeChildren.Length > 0)
        {
            bool isOnlyUpgrade = false;
            if (onlyUpgrades.Count > 0) {
                onlyUpgrades.Remove(upgradeObject);
                isOnlyUpgrade = true;
            } else upgrades.Remove(upgradeObject);

            foreach (UpgradeObject childUpgrade in upgradeObject.upgradeChildren)
            {
                if (isOnlyUpgrade) onlyUpgrades.Add(childUpgrade);
                else upgrades.Add(childUpgrade);
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
            case Upgrades.FreezeBullets: {
                print("Freeze bullets");
                PlayerShooting.Instance.FreezingBulletChanceIncrease(20);
                break;
            }
            case Upgrades.Rotating: {
                print("Rotation");
                RotatingWeapon.Instance.ActivateWeapon();
                break;
            }
        }
    }

    private void NextLevel() {
        level++;
        levelCounterText.text = "Level " + level;
    }

    private List<UpgradeObject> GetRandomUpgrades() {
        List<UpgradeObject> upgradesPossible = new List<UpgradeObject>(onlyUpgrades.Count > 0 ? onlyUpgrades : upgrades);
        List<UpgradeObject> upgradeResults = new List<UpgradeObject>();
        
        for (int i = 0; i < 3; i++)
        {
            UpgradeObject randomUpgrade = upgradesPossible[Random.Range(0, upgradesPossible.Count)];
            
            if (onlyUpgrades.Count <= 0 && upgrades.Count >= 3) upgradesPossible.Remove(randomUpgrade);
            else if (onlyUpgrades.Count >= 3) upgradesPossible.Remove(randomUpgrade);
            upgradeResults.Add(randomUpgrade);
        }

        return upgradeResults;
    }

}
