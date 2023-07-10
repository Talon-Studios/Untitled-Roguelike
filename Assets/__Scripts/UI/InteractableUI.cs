using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image border;
    public bool hovered = false;

    private bool usingBorder;

    void Start() {
        usingBorder = border != null;
        
        if (usingBorder) border.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        hovered = true;

        MousePointer.Instance.SetMouseState(MouseState.Interacting);
        if (usingBorder) border.gameObject.SetActive(hovered);
    }

    public void OnPointerExit(PointerEventData eventData) {
        hovered = false;

        MousePointer.Instance.SetMouseState(MouseState.Aiming);
        if (usingBorder) border.gameObject.SetActive(hovered);
    }

}
