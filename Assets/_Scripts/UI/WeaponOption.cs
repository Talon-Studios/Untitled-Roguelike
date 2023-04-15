using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponOption : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    public GunObject gun;

    public void OnPointerClick(PointerEventData eventData) {
        WeaponSelectManager.Instance.SetGameWeapon(gun);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        WeaponSelectManager.Instance.SetWeaponInfo(gun);
    }

}
