using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image border;

    void Start() {
        border.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        MousePointer.Instance.SetMouseState(MouseState.Interacting);
        if (border != null) border.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        MousePointer.Instance.SetMouseState(MouseState.Aiming);
        if (border != null) border.gameObject.SetActive(false);
    }

}
