using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeCard : MonoBehaviour, IPointerClickHandler
{

    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text characterNameText;
    
    [HideInInspector] public UpgradeObject upgrade;

    public void OnPointerClick(PointerEventData eventData) {
        UpgradeManager.Instance.ActivateUpgrade(upgrade);
        UpgradeManager.Instance.CloseUpgradesPanel();
        EnemyManager.Instance.EnemyBuff();
    }

}
