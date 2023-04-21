using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrades
{
    Fire,
    Ball
}

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private UpgradeObject[] upgrades;

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
        switch (upgradeObject.upgrade)
        {
            case Upgrades.Fire: {
                print("Fire");
                FireWeapon.Instance.ActivateWeapon();
                break;
            }
            case Upgrades.Ball: {
                print("Ball");
                BallWeapon.Instance.ActivateWeapon();
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
            if (upgrades.Length > 3) upgradesPossible.Remove(randomUpgrade);
            upgradeResults.Add(randomUpgrade);
        }

        return upgradeResults;
    }

    private float PercentOf(float number, float percentage) {
        return number / 100 * percentage;
    }

}
