using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : MonoBehaviour
{

    [Tooltip("How much XP you have to collect before getting an upgrade at the start of the game")]
    [SerializeField] private int startingUpgradeXP = 10;

    [Tooltip("How much more XP do you have to collect every upgrade")]
    [SerializeField] private int upgradeXPIncrement = 10;

    [Tooltip("How much force the XP has at the start to seperate it")]
    [SerializeField] private float xpStartForce = 5;

    [SerializeField] private Rigidbody2D xpPrefab;

    private int xp = 0;
    private int upgradeXP;

    #region Singleton
    
    static public XPManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        upgradeXP = startingUpgradeXP;
    }

    public void CreateXP(Vector2 position, int amount = 1) {
        for (int i = 0; i < amount; i++)
        {
            Rigidbody2D xpBody = Instantiate(xpPrefab, position, Quaternion.identity);
            xpBody.AddForce(xpStartForce * Random.insideUnitCircle.normalized, ForceMode2D.Impulse);
        }
    }

    public void GainXP(int amount) {
        xp += amount;
        if (xp >= upgradeXP)
        {
            UpgradeManager.Instance.SetupUpgradesPanel();
            upgradeXP += upgradeXPIncrement;
            xp = 0;
        }
    }

}