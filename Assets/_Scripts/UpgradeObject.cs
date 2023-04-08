using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade", fileName = "New Upgrade")]
public class UpgradeObject : ScriptableObject
{

    public string upgradeName;
    [TextArea] public string upgradeDescription;
    public Upgrades upgrade;

}
