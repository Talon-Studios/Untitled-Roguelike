using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public bool selected = false;
    public GunObject gun;
    public Color hoverColor;
    public RawImage weaponGraphics;

    [HideInInspector] public Image border;
    [HideInInspector] public Color originalColor;
    
    void Awake() {
        border = GetComponent<Image>();
        originalColor = border.color;

        weaponGraphics.texture = gun.graphics;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        border.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!selected)
        {
            border.color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        SelectManager.Instance.SetWeapon(this);
    }

}
