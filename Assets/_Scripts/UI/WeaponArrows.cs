using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponArrows : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private int direction = 1;

    public void OnPointerClick(PointerEventData eventData) {
        WeaponSelectManager.Instance.NextPage(direction);
    }

}
