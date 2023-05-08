using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{

    [Tooltip("How much XP you have to collect before getting an upgrade at the start of the game")]
    [SerializeField] private int startingUpgradeXP = 10;

    [Tooltip("How much more XP do you have to collect every upgrade")]
    [SerializeField] private int upgradeXPIncrement = 10;

    [Tooltip("How much force the XP has at the start to seperate it")]
    [SerializeField] private float xpStartForceMin = 5;
    [SerializeField] private float xpStartForceMax = 10;

    [SerializeField] private Rigidbody2D xpPrefab;

    [Header("UI")]
    [SerializeField] private Transform xpBarsParent;
    
    [Tooltip("How fast the bar fill animation is")]
    [SerializeField] private float barFillSmoothing = 0.02f;

    private int xp = 0;
    private int upgradeXP;
    private float targetBarFill;

    Image[] xpBars;

    #region Singleton
    
    static public XPManager Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        upgradeXP = startingUpgradeXP;
        xpBars = xpBarsParent.GetComponentsInChildren<Image>();
    }

    void Update() {
        foreach (Image bar in xpBars)
        {
            bar.fillAmount = Mathf.SmoothStep(bar.fillAmount, targetBarFill, barFillSmoothing);
            if (xp == upgradeXP)
            {
                bar.color = DynamicColorManager.Instance.Color;
            } else
            {
                bar.color = Color.white;
            }
        }
    }

    public void CreateXP(Vector2 position, int amount = 1) {
        for (int i = 0; i < amount; i++)
        {
            Rigidbody2D xpBody = Instantiate(xpPrefab, position, Quaternion.identity);
            float randomForce = Random.Range(xpStartForceMin, xpStartForceMax);
            xpBody.AddForce(randomForce * Random.insideUnitCircle.normalized, ForceMode2D.Impulse);
        }
    }

    public void ResetXP() {
        xp = 0;
        SetXPBars();
    }

    public void GainXP(int amount) {
        xp += amount;
        SetXPBars();
        if (xp >= upgradeXP)
        {
            StartCoroutine(UpgradeManager.Instance.SetupUpgradesPanel());
            upgradeXP += upgradeXPIncrement;

            xp = upgradeXP;
            SetXPBars();
        }
    }

    private void SetXPBars() {
        targetBarFill = 0.5f / upgradeXP * xp;
    }

}
